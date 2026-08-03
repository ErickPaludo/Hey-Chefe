using Financ.Domain.Objetos_de_Valor;
using Financ.Domain.Validacoes;
using Financ.Domain.Validacoes.Usuarios;
using Financ.Domain.Validacoes.Usuarios.Mensagens;
using System.Drawing;

namespace Financ.Domain.Entidades.Usuarios
{
    public sealed class Usuario : EntidadeBase
    {
        public Nome Nome { get; }
        public Email Endereco { get;}
        public Senha Senha { get; private set; }
      
        private Usuario(Nome nome, Email endereco, Senha senha)
        {
            ValidaNulo.Verifica(nome, MensagensUsuarios.NOME_NULO);
            ValidaNulo.Verifica(endereco, MensagensUsuarios.EMAIL_NULO);
            ValidaNulo.Verifica(senha, MensagensUsuarios.SENHA_NULA);

            Nome = nome;
            Endereco = endereco;
            Senha = senha;
        }
 
        public static Usuario Create(Nome nome, Email endereco, Senha senha)
        {
            return new Usuario(nome, endereco, senha);
        }

        public void AtualizaSenha(Senha senha)
        {
            ValidaNulo.Verifica(senha, MensagensUsuarios.SENHA_NULA);
            UsuariosValidacao.Verifica(Senha == senha, MensagensUsuarios.MESMA_SENHA);
            Senha = senha;
        }
    }
}
