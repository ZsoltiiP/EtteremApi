using Etterem.Models;
using Etterem.Models.DtoS;
using Etterem.Services.IRestaurant;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Etterem.Services
{
    public class KapcsoloService
    {
        private readonly EtteremContext _context;
        private readonly ResponseDto responseDto;
        public KapcsoloService(EtteremContext context, ResponseDto responseDTO)
        {
            _context = context;
            responseDto = responseDTO;
        }

        public async Task<object> PostNewRelation(AddRelationDto addRelationDto)
        {
            try
            {
                var relation = new Kapcsolo
                {
                    RendelesId = addRelationDto.RendelesId,
                    TermekekId = addRelationDto.TermekekId
                };

                if (relation != null)
                {
                    await _context.Kapcsolos.AddAsync(relation);
                    await _context.SaveChangesAsync();

                    responseDto.Message = "Sikeres összerendelés.";
                    responseDto.Result = relation;

                    return responseDto;
                }

                responseDto.Message = "Sikertelen összerendelés.";
                responseDto.Result = relation;

                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.Result = ex.Data;

                return responseDto;
            }
        }
    }
}
