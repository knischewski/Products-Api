using API.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/produtos")]
    public class ProductsController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(INotifier notifier,
                                  IProductRepository productRepository,
                                  IProductService productService,
                                  IMapper mapper) : base(notifier)
        {
            _productRepository = productRepository;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAll()
        {
            var products = _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsSuppliers());
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> GetById(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await GetProduct(id));

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imageName = Guid.NewGuid() + "_" + productViewModel.Image;

            if (!UploadFile(productViewModel.ImagemUpload, imageName)) return CustomResponse(productViewModel);

            productViewModel.Image = imageName;

            await _productService.Add(_mapper.Map<Product>(productViewModel));

            return CustomResponse(productViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Update(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                NotifyError("Os ids informados não são iguais");
                CustomResponse(productViewModel);
            }

            var productUpdate = await GetProduct(id);
            productViewModel.Image = productUpdate.Image;

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (productViewModel.ImagemUpload != null)
            {
                var imageName = Guid.NewGuid() + "_" + productViewModel.Image;
                if (!UploadFile(productViewModel.ImagemUpload, imageName))
                {
                    return CustomResponse(ModelState);
                }
                productUpdate.Image = imageName;
            }

            productUpdate.Name = productViewModel.Name;
            productUpdate.Description = productViewModel.Description;
            productUpdate.Value = productViewModel.Value;
            productUpdate.Active = productViewModel.Active;            

            await _productService.Update(_mapper.Map<Product>(productUpdate));

            return CustomResponse(productViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Delete(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null) return NotFound();

            await _productService.Delete(id);

            return CustomResponse(product);
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetProductSupplier(id));
        }

        private bool UploadFile(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotifyError("Forneça uma imagem para este produto!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgNome);

            if (System.IO.File.Exists(filePath))
            {
                NotifyError("Já existe um arquivo com este nome!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }


    }
}
