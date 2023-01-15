using AutoMapper;
using HappyCompany.Domain.Data;
using HappyCompany.Domain.Dtos;
using HappyCompany.Domain.Filters;


namespace HappyCompany.Application.Services
{
    public class WarehouseServiceHandler :IWarehouseServiceHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public WarehouseServiceHandler(IUnitOfWork unitOfWork, ILogger logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _mapper = mapper;
        }

        public async Task<Response> AddWarehouse(WarehouseDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _logger.Information("start Add new Warehouse", entity);
           
                var warehouse = _mapper.Map<Warehouse>(entity);
                await _unitOfWork.warehouseRepository.Add(warehouse);
                var result = _unitOfWork.Complete;
                if(result)
                    return new Response
                    {
                        HttpResponse = HttpResponse.Ok,
                        Message = "new Warehouse added successfully ",
                        Data = entity,
                        Succeeded = true
                    };
            
                _logger.Error($"an error Occured during save new Warehouse With exc message ={result}" );
                return new Response
                {
                    HttpResponse = HttpResponse.InternalServerErrror,
                    Message = "there is a problem Occured during save new Warehouse ",
                    Succeeded = false
                };
            
        }

        public async Task<Response> GetWarehouseDetails(int Id)
        {
            var warehouse = await _unitOfWork.warehouseRepository.Get(Id);
            if (warehouse == null)
                return new Response
                {
                    HttpResponse = HttpResponse.NotFound,
                    Message = $"there isn't warehouse with Id= {Id}",
                };

            _logger.Information($"Warehouse with id = {Id} is Exisiting");
            return new Response
            {
                HttpResponse = HttpResponse.Ok,
                Message = "returned user successfully ",
                Data = warehouse,
                Succeeded = true
            };
        }

        public async Task<Response> GetWarehouseListPage(PaginationQueryDto paginationQuery)
        {
            var pagination = _mapper.Map<paginationFilter>(paginationQuery);

            var list = await _unitOfWork.warehouseRepository.GetAll();
            var warehouseListPagedResp = await _unitOfWork.warehouseRepository.GetWarehousePaged(pagination?? new paginationFilter());
            if(warehouseListPagedResp.Data == null)
            {
                _logger.Information("There is No Warehouses Yet");
                warehouseListPagedResp.HttpResponse= HttpResponse.NotFound;
                warehouseListPagedResp.Message = "There is No Warehouses Yet";
                warehouseListPagedResp.Succeeded = false;

                return warehouseListPagedResp;
            }
            warehouseListPagedResp.HttpResponse = HttpResponse.Ok;
            warehouseListPagedResp.Succeeded = true;
            warehouseListPagedResp.Message = $"retuened {pagination.PageSize} Warehouses of page number {pagination.PageNumber} successfully";

            return warehouseListPagedResp;

        }
    }
}
