using API.Extensions;
using API.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/fornecedores")]
    public class SuppliersController : MainController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository,
                                   ISupplierService supplierService,
                                   IAddressRepository addressRepository,
                                   IMapper mapper,
                                   INotifier notifier,
                                   IUser user) : base (notifier, user)
        {
            _supplierRepository = supplierRepository;
            _supplierService = supplierService;
            _addressRepository = addressRepository;
            _mapper = mapper;
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

        [ClaimsAuthorize("Fornecedor","Adicionar")]
        [HttpPost]
        public async Task<ActionResult<SupplierViewModel>> Add(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.Add(_mapper.Map<Supplier>(supplierViewModel));

            return CustomResponse(supplierViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Update(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id)
            {
                NotifyError("O id informado é diferente do passado na query");
                return CustomResponse(supplierViewModel);
            }
            
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.Update(_mapper.Map<Supplier>(supplierViewModel));

            return CustomResponse(supplierViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Delete(Guid id)
        {
            var supplierViewModel = await GetSupplierAndAddress(id);

            if (supplierViewModel == null) return NotFound();

            await _supplierService.Delete(id);

            return CustomResponse(supplierViewModel);
        }

        [HttpGet("obter-endereco/{id:guid}")]
        public async Task<AddressViewModel> GetAddressById(Guid id)
        {
            var addressViewModel = _mapper.Map<AddressViewModel>(await _addressRepository.GetById(id));
            return addressViewModel;
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<ActionResult> UpdateAddress(Guid id, AddressViewModel addressViewModel)
        {
            if (id != addressViewModel.Id) return BadRequest();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.UpdateAddress(_mapper.Map<Address>(addressViewModel));

            return CustomResponse(addressViewModel);
        }

        private async Task<SupplierViewModel> GetSupplierProductsAddress(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierProductsAddress(id));
        }

        private async Task<SupplierViewModel> GetSupplierAndAddress(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAddress(id));
        }
    }
}
