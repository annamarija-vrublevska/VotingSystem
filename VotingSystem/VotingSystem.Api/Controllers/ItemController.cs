using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.Api.Entities;
using VotingSystem.Api.Models;
using VotingSystem.Api.Repositories.Interfaces;

namespace VotingSystem.Api.Controllers
{
    [Route("api/items")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IVoteHistoryRepository _voteHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemRepository itemRepository,
            IVoteHistoryRepository voteHistoryRepository,
            IMapper mapper,
            ILogger<ItemController> logger)
        {
            _itemRepository = itemRepository;
            _voteHistoryRepository = voteHistoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> Get()
        {
            var itemEntities = await _itemRepository.GetAllItemsAsync();
            return Ok(_mapper.Map<IEnumerable<ItemDto>>(itemEntities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> Get(long id)
        {
            var itemEntity = await _itemRepository.GetItemByIdAsync(id);

            if (itemEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ItemDto>(itemEntity));
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(ItemForCreationDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var currentUser = User.Identity?.Name;

            if (currentUser == null)
            {
                return BadRequest();
            }

            var itemEntity = _mapper.Map<Item>(item);
            itemEntity.UserName = currentUser;
            await _itemRepository.AddItemAsync(itemEntity);

            try
            {
                await _itemRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception while saving new item.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return Ok();
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, ItemForUpdateDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var currentUser = User.Identity?.Name;

            if (currentUser == null)
            {
                return BadRequest();
            }

            var itemEntity = await _itemRepository.GetItemByIdAsync(id);

            if (itemEntity == null)
            {
                return NotFound();
            }

            if (itemEntity.UserName != currentUser)
            {
                return BadRequest();
            }

            _mapper.Map(item, itemEntity);
            await _itemRepository.SaveChangesAsync();
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var currentUser = User.Identity?.Name;

            if (currentUser == null)
            {
                return BadRequest();
            }

            var itemEntity = await _itemRepository.GetItemByIdAsync(id);

            if (itemEntity == null)
            {
                return NotFound();
            }

            if (itemEntity.UserName != currentUser)
            {
                return BadRequest();
            }

            _itemRepository.DeleteItem(itemEntity);
            await _itemRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("vote/{id}")]
        public async Task<ActionResult> Vote(long id)
        {
            var currentUser = User.Identity?.Name;

            if (currentUser == null)
            {
                return BadRequest();
            }

            var itemEntity = await _itemRepository.GetItemByIdAsync(id);

            if (itemEntity == null)
            {
                return NotFound();
            }

            if (currentUser == itemEntity.UserName)
            {
                return BadRequest("You are not allowed to vote for your item.");
            }

            if (_voteHistoryRepository.HasAlreadyVoted(currentUser, id))
            {
                return BadRequest("You have already voted for this item.");
            }

            itemEntity.Volume++;
            await _itemRepository.SaveChangesAsync();
            await _voteHistoryRepository.UpdateItemVoteHistory(currentUser, id);
            return Ok("Your vote is received");
        }
    }
}
