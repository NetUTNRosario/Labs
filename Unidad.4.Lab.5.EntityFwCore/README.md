# Unidad 4 - Laboratorio 5

### Objetivos
Interiorizarse con conceptos de Entity Framework Core como lo son el contexto, los consultas y carga temprana de entidades.

### Pasos
1. Completar la implementación en la función correspondiente de la clase ***DataAccess**, por ejemplo:
``` c#
public class CursosRepositorio
{
    public IEnumerable<Materia> GetMaterias(int hsSemanales, int anioPlan) {...}
}
```
2. En la clase ***DataAccessTest*** ir a ***Prueba***/***Ejecutar todas las pruebas*** de la barra de herramientas de VS. Esto es para verificar que la implementación cumpla con las especificaciones requeridas. Por ejemplo en el caso anterior seria:
``` c#
public class CursosRepositorioTest
{
    [Fact]
    public void GetMateriasTest() {...}
}
```

### Ejercicios
> Recordar desechar la instancia de context una vez realizada la consulta mediante un bloque ```using``` en donde se cree una instancia del context mediante ```_contextFactory.CreateContext()```

> Para conocer los datos de la base de datos sobre los cuales los test serán evaluados revisar código de la clase ```TestDbSeed``` del proyecto ```DataAccessTest```
<details close>
<summary>Ver Datos en BD</summary>

```c#
var especialidades = new List<Especialidad>()
{
    new()
    {
        Descripcion = "Ingeniería en Sistemas de Información"
    },
    new()
    {
        Descripcion = "Ingeniería Química"
    },
    new()
    {
        Descripcion = "Ingeniería Eléctrica"
    },
    new()
    {
        Descripcion = "Ingeniería Mecánica"
    },
    new()
    {
        Id = 5,
        Descripcion = "Ingeniería Civil"
    }
};

var planes = new List<Plan>()
{
    new()
    {
        Anio = 2008,
        Especialidad = especialidades[0]
    },
    new()
    {
        Anio = 1995,
        Especialidad = especialidades[0]
    },
    new()
    {
        Anio = 1994,
        Especialidad = especialidades[3]
    },
    new()
    {
        Anio = 2009,
        Especialidad = especialidades[4]
    }
};

var materias = new List<Materia>
{
    new()
    {
        Descripcion = "Sistemas de Gestión",
        HsSemanales = 4,
        HsTotales = 136,
        Plan = planes[0]
    },
    new()
    {
        Descripcion = "Administración Gerencial",
        HsSemanales = 6,
        HsTotales = 102,
        Plan = planes[0]
    }
};
```

</details>
1. Traer las materias con menos de **x** horas semanales con el plan del año **z**, ordenados en forma descendente por horas semanales, incluyendo los datos del plan y la especialidad asociados a estas

2. Guardar una materia con el plan mas actual que este asociado con la especialidad que contenga el nombre parcial enviado como parámetro