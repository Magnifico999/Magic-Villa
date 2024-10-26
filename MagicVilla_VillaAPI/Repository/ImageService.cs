using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace MagicVilla_VillaAPI.Services
{
	public class ImageService
	{
		private readonly Cloudinary _cloudinary;
		public ImageService(IConfiguration config)
		{
			var acc = new Account
			(
				config["Cloudinary:CloudName"],
				config["Cloudinary:ApiKey"],
				config["Cloudinary:ApiSecret"]
			);
			_cloudinary = new Cloudinary(acc);
		}
		public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
		{
			var uploadresult = new ImageUploadResult();

			if (file.Length > 0)
			{
				using var stream = file.OpenReadStream();
				var uploadParams = new ImageUploadParams
				{
					File = new FileDescription(file.FileName, stream)
				};
				uploadresult = await _cloudinary.UploadAsync(uploadParams);
			}

			return uploadresult;
		}

		public async Task<DeletionResult> DeleteImageAsync(string publicId)
		{
			var deleteParams = new DeletionParams(publicId);
			var result = await _cloudinary.DestroyAsync(deleteParams);
			return result;
		}
	}
}