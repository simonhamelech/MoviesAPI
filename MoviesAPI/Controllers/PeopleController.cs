using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly ILogger<GenresController> logger;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly string containterName = "moveiseapipeople";

        public PeopleController(IRepository repository,
                                    ILogger<GenresController> logger,
                                    IMapper mapper,
                                    IFileStorageService fileStorageService)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
        }
        [HttpGet] // api/people
        public async Task<ActionResult<List<PersonDTO>>> Get()
        {
            logger.LogInformation("Getting all the people");

            var allPeople = await repository.GetAllPeople();
            var peopleDTO = mapper.Map<List<PersonDTO>>(allPeople);
            return peopleDTO;
        }
        [HttpGet("{Id:int}", Name = "getPerson")]
        public async Task<ActionResult<PersonDTO>> Get(int Id)
        {
            var person = await repository.GetPersonById(Id);

            if (person == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PersonDTO>(person));
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PersonCreationDTO personCreationDTO)
        {
            var person = mapper.Map<Person>(personCreationDTO);
            if (personCreationDTO.Picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await personCreationDTO.Picture.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(personCreationDTO.Picture.FileName);
                    person.Picture = await fileStorageService.SaveFile(content, extension, containterName, personCreationDTO.Picture.ContentType);
                }
            }
            await repository.AddPerson(person);
            var personDTO = mapper.Map<PersonDTO>(person);
            return new CreatedAtRouteResult("getPerson", new { personDTO.Id }, personDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PersonCreationDTO personCreationDTO)
        {
            var personDB = await repository.GetPersonById(id);
            if (personDB == null)
                return NotFound();
            personDB = mapper.Map(personCreationDTO, personDB);
            if (personCreationDTO.Picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await personCreationDTO.Picture.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(personCreationDTO.Picture.FileName);
                    personDB.Picture = await fileStorageService.EditFile(content, extension, containterName, personDB.Picture, personCreationDTO.Picture.ContentType);
                }
            }
            await repository.UpdatePerson(personDB);
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody]JsonPatchDocument<PersonPatchDTO> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();
            var entityFromDB = await repository.GetPersonById(id);
            if (entityFromDB == null)
                return NotFound();
            var entityDTO = mapper.Map<PersonPatchDTO>(entityFromDB);
            patchDocument.ApplyTo(entityDTO, ModelState);
            var isValid = TryValidateModel(entityDTO);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            mapper.Map(entityDTO, entityFromDB);
            await repository.UpdatePerson(entityFromDB);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await repository.DeletePerson(id);
            if (!exists)
                return NotFound();
            return NoContent();
        }
    }
}
