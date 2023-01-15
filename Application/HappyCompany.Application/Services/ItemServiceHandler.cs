using AutoMapper;
using HappyCompany.Domain.Dtos;
using HappyCompany.Domain.Filters;
using System.Linq.Expressions;

namespace HappyCompany.Application.Services
{
    public class ItemServiceHandler : IItemServiceHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ItemServiceHandler(IUnitOfWork unitOfWork, ILogger logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response> AddItem(Item entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _logger.Information("start Add new Item", entity);

             await _unitOfWork.itemRepository.Add(entity);
            var result = _unitOfWork.Complete;
            if(result)
                return new Response
                {
                    HttpResponse = HttpResponse.Ok,
                    Message = "new item added successfully ",
                    Data = entity,
                    Succeeded = true
                };
            
            _logger.Error($"an error Occured during save new Item With exec message = {result}");
            return new Response
            {
                HttpResponse = HttpResponse.InternalServerErrror,
                Message = "there is a problem Occured during save new item ",
                Succeeded = false
            };
            
        }

        public async Task<Response> GetItemListPage(PaginationQueryDto paginationQuery,int WarehouseId)
        {
            var pagination = new paginationFilter();//_mapper.Map<paginationFilter>(paginationQuery);
            Expression<Func<Item, bool>> exp = sub => sub.warehouseId.Equals(WarehouseId);

            var itemListPagedResp = await _unitOfWork.itemRepository.GetItemPaged(pagination ?? new paginationFilter(),exp);
            if (itemListPagedResp.Data == null)
            {
                _logger.Information("There is No Warehouses Yet");
                itemListPagedResp.HttpResponse = HttpResponse.NotFound;
                itemListPagedResp.Message = "There is No Warehouses Yet";
                itemListPagedResp.Succeeded = false;

                return itemListPagedResp;
            }
            itemListPagedResp.HttpResponse = HttpResponse.Ok;
            itemListPagedResp.Succeeded = true;
            itemListPagedResp.Message = $"retuened {pagination.PageSize} Warehouses of page number {pagination.PageNumber} successfully";

            return itemListPagedResp;
        }
    }
}
