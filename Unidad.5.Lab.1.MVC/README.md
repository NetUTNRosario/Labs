# Unidad 4 - Laboratorio 5

### Objetivos
Interiorizarse con conceptos de asp net core mvc como lo son: controladores, vistas, modelo, validacion por data annotations, ruteo por código o por vistas, viewmodels, vistas de error, layout (o master-view) e inyección de dependencias.

### Pasos
1. En la clase ```HomeController``` accion ```Index``` redireccionar a la accion ```List``` del controlador ```¨MateriaController```. Para esto usar el metodo 
```c#
RedirectToAction(string actionName, string controllerName)
```

2. En la clase ```Startup``` ir al metodo ```ConfigureServices(IServiceCollection services)``` y agregar los siguientes servicios (repositorios que se encargaran de manejar las colecciones en memoria requeridas para este ejercicio) para ser proveidos por el contenedor de dependencias
```c#
services.AddSingleton<IMateriaRepository, MateriaRepository>();
services.AddSingleton<IPlanRepository, PlanRepository>();
```
> Estos servicios son declarados como singletons por lo que sera proveida siempre la misma instancia de cada uno para cualquier clase que los solicite y por lo tanto la coleccion en memoria que estos alojan perdurara a pesar de que los controladores se reinstancian en cada request (ejemplo, refrescar el navegador de /Materia/List involucra volver a instanciar todo lo que esta declarado en el constructor)

3. En la clase ```MateriaController``` agregar los siguientes campos privados de solo lectura y setearlos en el constructor
```c#
public MateriaController(ILogger<MateriaController> logger, IMateriaRepository materiaRepository, IPlanRepository planRepository)
```
> El contenedor de dependencias proveera las instancias correspondientes de estas clases como se declaro en el paso anterior

4. En la accion ```List``` devolver la vista enviandole los datos de todos las materias pre-cargadas mediante ```_materiaRepository```
```c#
return View(_materiaRepository.GetAll())
```

5. Agregar el contenido de la vista correspondiente ubicada en la carpeta ***Views/Materia***, mostrando un listado ```<ul>``` de cards de bootstrap (clase ***.card***, esto ya viene configurado con el proyecto default de inicio). Asegurarse que los atributos Descripcion, Plan, HsSemanales y HsTotales sean mostrados (estos ultimos dos utilizando la clase ***.card-subtitle*** de bootstrap, ademas se aconseja usar a Descripcion como un ***.card-title*** y para el resto ***.card-text***). En cuanto al uso de directivas razor para iterar en la coleccion usar 
```c#
// Solo al inicio del archivo
@model IEnumerable<Materia>

@foreach (Materia mat in Model){@mat.Descripcion}
```
> Estas directivas ```@mat.{atributo}``` van a popular con los datos enviados desde el controlador el html enviado ante cada request (esto sucede en el servidor, osea su maquina en el puerto 5000). Estas estan fuertemente tipadas (lo cual hace que se provean sugerencias al escribir) gracias a la directiva ```@Model IEnumerable<Materia>``` que se encuentra al inicio del archivo, lo cual determina que tipo de dato se aceptara para ser enviado por el controlador en la accion correspondiente

<details close>
<summary>Ver Vista Completa</summary>

```html
@model IEnumerable<Materia>

<ul class="list-group">
    @foreach (Materia mat in Model)
    {
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">@mat.Descripcion</h5>
                <h6 class="card-subtitle mb-2 text-muted">
                    @Html.DisplayNameFor(model => mat.HsSemanales): @mat.HsSemanales -
                    @Html.DisplayNameFor(model => mat.HsTotales): @mat.HsTotales
                </h6>
                <p class="card-text">@mat.Plan.Especialidad <span class="text-info">@mat.Plan.Anio</span></p>
                <a class="btn btn-primary" asp-area="" asp-action="Edit" asp-route-id="@mat.Id">Editar</a>
            </div>
        </div>
    }
</ul>
```

</details> 

6. En la accion ```Edit(int? id)``` devolver la vista inicial para el formulario de edicion, enviandole el viewmodel ```EditMateriaViewModel```(conjunto de datos especifico para esta vista) que toma tomo argumentos requeridos la materia a editar (obtenible desde ```_materiaRepository.GetOne(int id)```) y los planes de estudio disponibles (se puede obtener desde ```_planesRepository.GetAll()```). Recordar validar que el id enviado como argumento a la accion no sea nulo (ya que esto puede ocurrir como bien denota el tipo entero nullable ```int?```) y que la materia devuelta por ```_materiaRepository.GetOne(int id)``` no sea una referencia nula (observar que su tipo de retorno en la firma del metodo es ```Materia?```, un tipo de referencia nullable), si alguna de estas situaciones sucede devolver el resultado ```NotFound()```
<details close>
<summary>Ver Codigo</summary>

```c#
if (id == null) return NotFound();
Materia? materia = _materiaRepository.GetOne((int)id);
if (materia == null) return NotFound();
return View(new EditMateriaViewModel(materia, _planRepository.GetAll()));
```

</details> 

7. Agregar la vista correspondiente, utilizando un formulario de edicion ```<form asp-action="Edit">``` donde como se observa se utilizaran directivas ***asp-{...}*** para agregarle propiedades a la vista que solo seran utilizadas para renderizar el html (con los datos correspondientes) a enviar por el servidor. Usar la directiva razor ```asp-for``` tanto en los input como en los labels contenidos en los ```<div class="form-group"></div>``` (***.form-group*** es una clase de bootstrap). Como este es un formulario de edicion dejar los values de cada ```<input/>``` con el valor del atributo de la materia ```@@Model.Materia.{Atributo}```. Ya que hay un conjunto finito de planes que pueden ser asociados a una materia es conveniente optar por un elemento ```<select></select>``` que muestre las opciones disponibles (para designar cuales son estas utilizar la directiva ```asp-items="Model.Planes"```, donde ***Model.Planes*** es un campo del viewmodel enviado por el controlador). Finalmente, agregar un input de tipo submit con las clases ```.btn .btn-primary``` de bootstrap. Recordar agregar un campo para preservar el atributo id a ser enviado en la accion nuevamente POST
```html
<input asp-for="Materia.Id" type="hidden" />
```
<details close>
<summary>Ver Vista Completa</summary>

```html
@model EditMateriaViewModel

<div class="row">
    <div class="col-md-4">
        <form asp-action="Edit">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <input asp-for="Materia.Id" type="hidden" />
            <div class="form-group">
                <label asp-for="Materia.Descripcion" class="form-label">Descripcion</label>
                <input asp-for="Materia.Descripcion" class="form-control" placeholder="@Model.Materia.Descripcion"
                       value="@Model.Materia.Descripcion" />
            </div>

            <div class="form-group">
                <label asp-for="Materia.HsSemanales" class="form-label">Horas Semanales</label>
                <input asp-for="Materia.HsSemanales" type="number" class="form-control" placeholder="@Model.Materia.HsSemanales"
                       value="@Model.Materia.HsSemanales">
                <span asp-validation-for="Materia.HsSemanales" class="text-danger"></span>
            </div>

            <div class="form-group">
                <label asp-for="Materia.HsTotales" class="form-label">Horas Totales</label>
                <input asp-for="Materia.HsTotales" type="number" class="form-control" placeholder="@Model.Materia.HsTotales"
                       value="@Model.Materia.HsTotales">
                <span asp-validation-for="Materia.HsTotales" class="text-danger"></span>
            </div>

            <select class="form-group" asp-for="Materia.PlanId" asp-items="Model.Planes"></select>

            <div class="form-group">
                <button type="submit" class="btn btn-primary">Save</button>
            </div>
        </form>
    </div>
</div>
```

</details> 

8. En la entidad ```Materia``` agregar validaciones para lo siguiente
- La descripcion de una materia debe tener este 3 y 20 caracteres
- Las horas semanales de una materia deben ser entre 2 y 6. Ademas, este campo se debe mostrar como "Horas Semanales" 
- Las horas totales no deben sobrepasar las 150 y tienen que ser superiores a 90 horas. Este campo se debe mostrar como "Horas Totales""
- Los campos Descripcion, HsSemanales, HsTotales y PlanId son requeridos
> Para esto usar las data annotations: [StringLength(maximoCaracteres, MinimumLength = minimoCaracteres)]
, [Range(inferior, superior)], [Required], [Display(Name = "nombreX")]

Al terminar agregar los spans siguientes como ultimo contenido de cada ```<div class="form-group">...</div>``` de la vista ***Edit***. Lo cual servira para mostrar los mensajes de validacion correspondientes a cada campo directamente abajo de este, esto sera ejecutado primero en cliente por lo que no se permitira hacer la request al servidor si hay errores de validacion y solo se hara la validacion en servidor si javascript se encuentra desactivado o por otras razones.
```html
// Unicamente al inicio del form
<div asp-validation-summary="ModelOnly" class="text-danger"></div>

// Dentro de cada .form-group
<div class="form-group">
    ...
    <span asp-validation-for="Materia.{Atributo}" class="text-danger"></span>
</div>

```
<details close>
<summary>Ver Código</summary>

```c#
    public int Id { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string Descripcion { get; set; }
    [Required]
    [Range(2, 6)]
    [Display(Name = "Horas Semanales")]
    public int HsSemanales { get; set; }
    [Required]
    [Range(90, 150)]
    [Display(Name = "Horas Totales")]
    public int HsTotales { get; set; }
    [Required]
    public int PlanId { get; set; }
    public Plan? Plan { get; set; }
```

</details>

<details close>
<summary>Ver Vista Completa</summary>

```html
@model EditMateriaViewModel

<div class="row">
    <div class="col-md-4">
        <form asp-action="Edit">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <input asp-for="Materia.Id" type="hidden" />
            <div class="form-group">
                <label asp-for="Materia.Descripcion" class="form-label">Descripcion</label>
                <input asp-for="Materia.Descripcion" class="form-control" placeholder="@Model.Materia.Descripcion"
                       value="@Model.Materia.Descripcion" />
            </div>

            <div class="form-group">
                <label asp-for="Materia.HsSemanales" class="form-label">Horas Semanales</label>
                <input asp-for="Materia.HsSemanales" type="number" class="form-control" placeholder="@Model.Materia.HsSemanales"
                       value="@Model.Materia.HsSemanales">
                <span asp-validation-for="Materia.HsSemanales" class="text-danger"></span>
            </div>

            <div class="form-group">
                <label asp-for="Materia.HsTotales" class="form-label">Horas Totales</label>
                <input asp-for="Materia.HsTotales" type="number" class="form-control" placeholder="@Model.Materia.HsTotales"
                       value="@Model.Materia.HsTotales">
                <span asp-validation-for="Materia.HsTotales" class="text-danger"></span>
            </div>

            <select class="form-group" asp-for="Materia.PlanId" asp-items="Model.Planes"></select>

            <div class="form-group">
                <button type="submit" class="btn btn-primary">Save</button>
            </div>
        </form>
    </div>
</div>
```

</details>
9. En la accion ```Edit(int id, [Bind("Id, Descripcion, HsSemanales, HsTotales, PlanId")]Materia materia)```, osea la parte que ocurre al hacer submit (POST) del formulario obtenido en el GET de la accion, actualizar la materia del id correspondiente con el metodo ```_materiaRepository.Update(Materia materia)``` y redirigir a la accion ```List```. Recordar comprobar si no se pasaron las validaciones con ```!ModelState.IsValid```, si ocurre re-renderizar la vista devolviendo ```View(new EditMateriaViewModel(...))``` al igual que en el GET y si el id enviado en la ruta no corresponde al enviado en los datos del formulario, si esto ultimo sucede devolver el resultado ```NotFound()```)
<details close>
<summary>Ver Código</summary>

```c#
if (id != materia.Id) return NotFound();
if (!ModelState.IsValid)
{
    return View(new EditMateriaViewModel(materia, _planRepository.GetAll()));
}

_materiaRepository.Update(materia);
return RedirectToAction("List");
```

</details>

10. Agregar el codigo para el viewmodel ```CreateMateriaViewModel``` correspondiente a la accion ```Create```. Este es muy similar al viewmodel correspondiente a la accion ```Edit``` con la salvedad que la propiedad ```Materia``` puede ser nula (al hacer el GET de esta accion siempre lo es, ya que es un formulario de creacion por lo que todos sus datos se encuentran vacios), para esto denotar el tipo de referencia nullable ```Materia?``` cuando sea necesario. Ademas, aqui no se preselecciona ninguna ```<option />``` del ```<select />```, ya que esta no es una materia previamente existente y por lo tanto no tiene ningun plan asignado.

<details close>
<summary>Ver Codigo</summary>

```c#
[HttpGet]
public IActionResult Create()
{
    return View(new CreateMateriaViewModel(null, _planRepository.GetAll()));
}
----------------------------------------------------------------------------
public Materia? Materia { get; }
public List<SelectListItem> Planes { get; }
public CreateMateriaViewModel(Materia? materia, IEnumerable<Plan> planes)
{
    Materia = materia;
    Planes = planes
        .Select(p => new SelectListItem
        { Text = $"{p.Especialidad}:{p.Anio}", Value = p.Id.ToString() }
        ).ToList();
}
```

</details>

11. Proceder de la misma manera que con la accion POST de ***Edit***, utilizando el metodo ```_materiaRepository.Add(Materia materia)```. Recordar revisar si las validaciones no fueron exitosas.
<details close>
<summary>Ver Codigo</summary>

```c#
if (!ModelState.IsValid)
{
    return View(new CreateMateriaViewModel(materia, _planRepository.GetAll()));
}

_materiaRepository.Add(materia);
return RedirectToAction("List");
```

</details>

13. Para la vista correspondiente a la accion ***Create*** proceder de forma similar a lo realizado para la vista ***Edit***.
<details close>
<summary>Ver Vista Completa</summary>

```html
<hr />
<div class="row">
    <div class="col-md-4">
        <form asp-action="Create">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <div class="form-group">
                <label asp-for="Materia.Descripcion" class="control-label"></label>
                <input asp-for="Materia.Descripcion" class="form-control" />
                <span asp-validation-for="Materia.Descripcion" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Materia.HsSemanales" class="control-label"></label>
                <input asp-for="Materia.HsSemanales" class="form-control" />
                <span asp-validation-for="Materia.HsSemanales" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Materia.HsTotales" class="control-label"></label>
                <input asp-for="Materia.HsTotales" class="form-control" />
                <span asp-validation-for="Materia.HsTotales" class="text-danger"></span>
            </div>
            <select class="form-group" asp-for="Materia.PlanId" asp-items="Model.Planes"></select>
            <div class="form-group">
                <input type="submit" value="Create" class="btn btn-primary" />
            </div>
        </form>
    </div>
</div>
```

</details>

14. En el proyecto ***Test*** clase ```IntegrationTestWeb``` ir a ***Prueba***/***Ejecutar todas las pruebas*** de la barra de herramientas de VS. Esto es para verificar que la implementación cumpla con las especificaciones requeridas. Por ejemplo para la accion ***/Materia/List***:
``` c#
[Fact]
public async Task VisitRootPage_ShouldRenderTwoMateriaCardsAndTheFirstOneMustHaveCertainCardSubtitle()
```
> Cada uno de estos test revisara el html enviado por cada accion de los controladores y chequeara que esten ciertos elementos previamente solicitados, como pueden ser las secciones de validacion o las opciones del select