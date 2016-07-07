using System;
using V1.Dominio;

namespace V1.Repositorios
{
    public interface IRepositorio<T> where T : Entidade, new()
    {
        void Salvar(Entidade entidade);

        T BuscarPor(Guid id);
    }
}