using API.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/fornecedores")]
    public class SuppliersController : MainController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        private readonly INotifier _notifier;

        public SuppliersController(ISupplierRepository supplierRepository,
                                   IMapper mapper,
                                   INotifier notifier) : base (notifier)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
            _notifier = notifier;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierViewModel>>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAll());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> GetById(Guid id)
        {
            var result = await GetSupplierProductsAddress(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierViewModel>> Add(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.Add(_mapper.Map<Supplier>(supplierViewModel));

            return CustomResponse(supplierViewModel);
        }

        private async Task<SupplierViewModel> GetSupplierProductsAddress(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierProductsAddress(id));
        }
    }
}
