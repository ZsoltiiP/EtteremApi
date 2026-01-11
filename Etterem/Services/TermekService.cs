using Etterem.Models;
using Etterem.Services.IRestaurant;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Etterem.Services
{
    public class TermekService : ITermek
    {
        private readonly EtteremContext _context;
        private readonly ResponseDto responseDto;
        public TermekService(EtteremContext context, ResponseDto responseDTO)
        {
            _context = context;
            responseDto = responseDTO;
        }
        public async Task<object> GetTermek()
        {
            try
            {
                var response = await _context.Termekeks
                    .Select(x => new { x.Etel, x.Ar })
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
    }
}
