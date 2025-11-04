using System;
using System.Collections.Generic;

public class categoria
{
    int id;
    string nome;
    string descricao;
    
}

public class utilizador
{
    int id;
    string nome;
    string email;
    string senha;
    
}

public enum perfil
{
    Administrador,
    UtilizadorComum
}