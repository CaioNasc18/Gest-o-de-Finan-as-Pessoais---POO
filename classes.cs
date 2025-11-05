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
    Perfil perfil;
}

public class transacao
{
    int id;
    string descricao;
    float valor;
    DateTime data;
    int categoriaId;
    
}
    
public enum Perfil
{
    Administrador,
    Utilizador_Normal
}
