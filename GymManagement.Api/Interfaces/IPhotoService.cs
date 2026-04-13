using CloudinaryDotNet.Actions;

namespace GymManagement.Api.Interfaces;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile photo);
    Task<DeletionResult> DeletePhotoAsync(string photoId);
}