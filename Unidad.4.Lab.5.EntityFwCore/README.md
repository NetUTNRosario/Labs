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
1. Traer las materias con menos de **x** horas semanales con el plan del año **z**, ordenados en forma descendente por horas semanales, incluyendo los datos del plan y la especialidad asociados a estas

2. Guardar una materia con el plan mas actual que este asociado con la especialidad que contenga el nombre parcial enviado como parámetro