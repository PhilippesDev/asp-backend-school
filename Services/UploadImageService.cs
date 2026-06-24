namespace api_gestion_ecole.Services
{
    public class UploadImageService
    {
        private readonly IWebHostEnvironment _env;
        public UploadImageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> SauvegarderImageAsync(IFormFile file, string folderName)
        {
            string rootPath = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            string dossierUploads = Path.Combine(rootPath, $"images/{folderName}");
            
            if (!Directory.Exists(dossierUploads)) Directory.CreateDirectory(dossierUploads);

            string nomFichierUnique = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string cheminComplet = Path.Combine(dossierUploads, nomFichierUnique);

            using (var stream = new FileStream(cheminComplet, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return nomFichierUnique;
        }

        public void SupprimerImagePhysiqueAsync(string? nomFichier, string folderName)
        {
            if (string.IsNullOrEmpty(nomFichier)) return;

            string cheminComplet = Path.Combine(_env.WebRootPath, $"images/{folderName}", nomFichier);
            
            if (System.IO.File.Exists(cheminComplet))
            {
                System.IO.File.Delete(cheminComplet);
            }
        }
        
    }
}