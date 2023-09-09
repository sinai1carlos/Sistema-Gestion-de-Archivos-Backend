using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_GestionArchivos_Backend.Models;

namespace Sistema_GestionArchivos_Backend.Controllers
{
    public class ArchivoController : Controller
    {
        private readonly ArchivosdbContext _context;
        
        public ArchivoController(ArchivosdbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var archivos = await _context.Archivos.ToListAsync();
            return View(archivos);
        }

        public IActionResult Cargar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cargar(Archivo archivo,IFormFile archivoSubido)
        {
            if(archivoSubido != null && archivoSubido.Length > 0)
            {
                //guardamos el archivo en el sistema  de archivos o  en la ubicacion deseada
                var rutaArchivo = Path.Combine("Ruta_de_almacenamiento", archivoSubido.FileName);
                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    await archivoSubido.CopyToAsync(stream);
                }

                archivo.Ruta = rutaArchivo;

                _context.Add(archivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(archivo);
        }

        public async Task<IActionResult> Descargar(int id)
        {
            var archivo = await _context.Archivos.FirstOrDefaultAsync(a => a.IdArchivo == id);
            if(archivo == null)
            {
                return NotFound();
            }

            var rutaArchivo = archivo.Ruta;//obtiene la ruta de archivos desde la base de datos

            //verifica que el archivo exista en la ubicacion especificada
            if (!System.IO.File.Exists(rutaArchivo))
            {
                return NotFound();//archivo no encontrado en la ubicacion especifica
            }

            //Define el tipo MIME  del archivo
            var contentType = "application/octet-stream";

            //obtiene el nombre del archivo desde la ruta completa
            var filename = Path.GetFileName(rutaArchivo);

            //lee el archivo en bytes
            var bytes = await System.IO.File.ReadAllBytesAsync(rutaArchivo);
            
            //Devuelve el archivo como resultado de la accion
            return File(bytes,contentType,filename);
        }
    }
}
