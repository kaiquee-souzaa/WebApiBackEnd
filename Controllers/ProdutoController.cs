using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBackEnd.Models;
using WebApiBackEnd.Data;
using System;

namespace WebApiBackEnd.Controllers {

    [Route("v1/produtos")]
    public class ProdutoController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Produto>>> GetAll([FromServices]DataContext context) 
        {
            var produtos = await context
                .Produtos
                .AsNoTracking()
                .ToListAsync();
            return Ok(produtos);
        }
        
        [HttpGet] // v1/produtos/produto/nomeProduto
        [Route("produto/{nomeProduto}")]
        public async Task<ActionResult<Produto>> GetByName(string nomeProduto,
                                                                [FromServices]DataContext context) 
        {
            var produtos = await context
                .Produtos
                .AsNoTracking()
                .Where(x => x.Nome == nomeProduto)
                .ToListAsync();
            return Ok(produtos);
        }

        [HttpPost]
        [Route("inserirProduto")]
        public async Task<ActionResult<Produto>> Post([FromBody]Produto modelProduto,
                                                      [FromServices]DataContext context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Produtos.Add(modelProduto);
                await context.SaveChangesAsync();
                return Ok(modelProduto);
            }
            catch
            {
                return BadRequest(new { message  = "Não foi possível criar o produto" });
            }
        }

        [HttpPut]
        [Route("alterarProduto/{codigoProduto:int}")]
        public async Task<ActionResult<List<Produto>>> Put(int codigoProduto,
                                                            [FromBody]Produto modelProduto,
                                                            [FromServices]DataContext context)
        {
            // Verifica se o id informado é o mesmo do modelo
            if (codigoProduto != modelProduto.Id)
            {
                return NotFound(new { message  = "Produto não encontrada" });
            }
            
            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Entry<Produto>(modelProduto).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(modelProduto);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message  = "Este registro já foi atualizado" });
            }
            catch (Exception)
            {
                return BadRequest(new { message  = "Não foi possível atualizar o produto" });
            }
        }

        [HttpDelete]
        [Route("excluirProduto/{codigoProduto:int}")]
        public async Task<ActionResult<List<Produto>>> Delete(int codigoProduto,
                                                                [FromServices]DataContext context)
        {
            var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == codigoProduto);
            if (produto == null)
            {
                return NotFound(new { message  = "Produto não encontrado" });
            }
            
            try
            {
                context.Produtos.Remove(produto);
                await context.SaveChangesAsync();
                return Ok(new { message  = "Produto removido com sucesso" });
            }
            catch (Exception)
            {
                return BadRequest(new { message  = "Não  foi possível remover o produto" });
            }
        }
    }
}