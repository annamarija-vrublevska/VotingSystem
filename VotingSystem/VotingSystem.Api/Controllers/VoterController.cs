using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Api.Entities;
using VotingSystem.Api.Logic;
using VotingSystem.Api.Models;

namespace VotingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/voters")]
    public class VoterController : ControllerBase
    {
        private readonly IVoterRepository _voterRepository;
        private readonly IValidator<Voter> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public VoterController(IVoterRepository voterRepository, 
            IValidator<Voter> validator, 
            IMapper mapper, 
            ILogger logger)
        {
            _voterRepository = voterRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoterDto>>> Get()
        {
            var voterEntities = await _voterRepository.GetAllVotersAsync();
            return Ok(_mapper.Map<IEnumerable<VoterDto>>(voterEntities));
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<VoterDto>> Get(string userName)
        {
            var voterEntity = await _voterRepository.GetVoterByUserNameAsync(userName);
            return Ok(_mapper.Map<VoterDto>(voterEntity));
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(VoterDto voter)
        {
            if (await _voterRepository.VoterExists(voter.UserName))
            {
                return BadRequest("User name already exists.");
            }

            var voterEntity = _mapper.Map<Voter>(voter);

            var validationResult = await _validator.ValidateAsync(voterEntity);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _voterRepository.AddVoterAsync(_mapper.Map<Voter>(voter));

            try
            {
                await _voterRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception while creating new voter.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return Ok();
        }

        [HttpPut("userName")]
        public async Task<ActionResult> Edit(string userName, VoterForUpdateDto voter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var voterEntity = await _voterRepository.GetVoterByUserNameAsync(userName);
            if (voterEntity == null)
            {
                return NotFound();
            }
            
            _mapper.Map(voter, voterEntity);

            var validationResult = await _validator.ValidateAsync(voterEntity);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _voterRepository.SaveChangesAsync();

            return Ok();
        }
        
        [HttpDelete("{userName}")]
        public async Task<ActionResult> Delete(string userName)
        {
            var voter = await _voterRepository.GetVoterByUserNameAsync(userName);
            if (voter == null)
            {
                return NotFound();
            }

            _voterRepository.DeleteVoter(voter);

            return Ok();
        }
    }
}