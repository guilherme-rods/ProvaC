using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class ServicoService
    {
        private readonly LojaDbContext _dbContext;

        public ServicoService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Servico>> GetAllServicosAsync()
        {
            return await _dbContext.Servicos.ToListAsync();
        }

        public async Task<Servico?> GetServicoByIdAsync(int id)
        {
            return await _dbContext.Servicos.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddServicoAsync(Servico servico)
        {
            _dbContext.Servicos.Add(servico);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateServicoAsync(int id, Servico servico)
        {
            var existingServico = await _dbContext.Servicos.FirstOrDefaultAsync(s => s.Id == id);
            if (existingServico != null)
            {
                existingServico.Preco = servico.Preco;
                existingServico.Status = servico.Status;
                
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteServicoAsync(int id)
        {
            var servico = await _dbContext.Servicos.FirstOrDefaultAsync(s => s.Id == id);
            if (servico != null)
            {
                _dbContext.Servicos.Remove(servico);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
