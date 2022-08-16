using Xunit;
using Moq;
using Chapter.Interfaces;
using Chapter.Models;
using Chapter.ViewModels;
using Chapter.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace TesteIntagracao.Controllers
{
    public class LoginControllerTest
    {
        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {
            //Preparação (ARRANGE)

            var repositorioEspelhado = new Mock<IUsuarioRepository>();

            repositorioEspelhado.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())). Returns((Usuario)null);

            LoginViewModel dados = new LoginViewModel();
            dados.Email = "jullys@email.com";
            dados.Senha = "1234";

            var controller = new LoginController(repositorioEspelhado.Object);

            //Ação (ACT)

            var resultado = controller.Login(dados);

            //Verificação (ASSERT)

            Assert.IsType<UnauthorizedObjectResult>(resultado);
        }

        [Fact]

        public void LoginController_Retornar_Token()
        {
            //Preparação (ARRANGE)

            Usuario usuarioRetorno = new Usuario();
            usuarioRetorno.Email = "jullys@email.com";
            usuarioRetorno.Senha = "1234";
            usuarioRetorno.Tipo = "1";
            usuarioRetorno.Id = 1;

            var repositorioEspelhado = new Mock<IUsuarioRepository>();

            repositorioEspelhado.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetorno);


            LoginViewModel dados = new LoginViewModel();
            dados.Email = "jullys@email.com";
            dados.Senha = "1234";

            var controller = new LoginController(repositorioEspelhado.Object);

            string issuerValido = "chapter.webapi";

            //Ação (ACT)

            OkObjectResult resultado = (OkObjectResult)controller.Login(dados);

            string tokenstring = resultado.Value.ToString().Split(' ')[3];

            var jwtHandle = new JwtSecurityTokenHandler();

            var tokenJwt = jwtHandle.ReadJwtToken(tokenstring);

            //Verificação (ASSERT)

            Assert.Equal(tokenstring, tokenJwt.Issuer);

        }
    }
}