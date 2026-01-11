using Etterem.Models;
using Etterem.Models.DtoS;
using Etterem.Services.IRestaurant;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Etterem.Services
{
    public class RendelesService : IRendeles
    {
        private readonly EtteremContext _context;
        private readonly ResponseDto responseDto;
        public RendelesService(EtteremContext context, ResponseDto responseDTO)
        {
            _context = context;
            responseDto = responseDTO;
        }
        public async Task<object> GetAllRendeles()
        {
            try
            {
                var response = await _context.Rendeles.ToListAsync();
                responseDto.Message = "Sikeres lekérdezés";
                responseDto.Result = response;

                return responseDto;
            }
            catch (Exception ex)
            {

                responseDto.Message = ex.Message;
                responseDto.Result = null;

                return responseDto;
            }
        }

        public async Task<object> GetAllRendelesWithCard()
        {
            try
            {
                var response = await _context.Rendeles
                    .Where(x => x.Fizetesimod == "Kártya")
                    .Select(x => x.Id)
                    .ToListAsync();

                responseDto.Message = "Sikeres lekérdezés";
                responseDto.Result = response;

                return responseDto;
            }
            catch (Exception ex)
            {

                responseDto.Message = ex.Message;
                responseDto.Result = null;

                return responseDto;
            }
        }

        public async Task<object> GetAllRendelesWithFood()
        {
            try
            {
                var response = await _context.Rendeles
                    .Include(x => x.Kapcsolos)
                    .ThenInclude(x => x.Termekek)
                    .ToListAsync();


                var food = response
                   .Select(x => new {
                       x.Asztalszam,
                       Termekek = x.Kapcsolos
                   .Select(y => y.Termekek.Etel)
                   })
                   .OrderBy(x => x.Asztalszam)
                   .GroupBy(x => x.Asztalszam)
                   .ToList();

                responseDto.Message = "Sikeres lekérdezés";
                responseDto.Result = food;

                return responseDto;
            }
            catch (Exception ex)
            {

                responseDto.Message = ex.Message;
                responseDto.Result = null;

                return responseDto;
            }
        }
    }
}
