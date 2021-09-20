# Unidad 4 - Laboratorio 5

### Objetivos
Interiorizarse con conceptos de asp net core mvc como lo son: controladores, vistas, modelo, validaciones en cliente y en servidor, ruteo por código o por vistas, viewmodels, vistas de error, layout (o master-view) e inyección de dependencias.

### Pasos
1. En la clase ```HomeController``` accion ```Index``` redireccionar a la accion ```List``` del controlador ```Materia```. Para esto usar el metodo 
```c#
RedirectToAction(string actionName, string controllerName)
// Es decir 
RedirectToAction(controllerName: "Materia", actionName: "List") 
// Aclaracion: al utilizar named parameters "actionName:" se puede alterar el orden los argumentos
```
> Toda clase controlador tiene como sufijo ***Controller*** como convencion de framework, como en el caso de ***Materia{Controller}***, lo cual es eliminado en el ruteo por ejemplo (quedando la url ***"/Materia/List"*** en lugar de ***"/MateriaController/List"***, es decir sigue la convencion de ruta ***"/{controlador}/{accion}/{id?}"*** declarada en la clase ```Startup```)

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

![image](https://user-images.githubusercontent.com/41701343/132447918-4167529f-bbed-49da-82ad-6e6e33590616.png)

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

7. Agregar la vista correspondiente, utilizando un formulario de edicion ```<form asp-action="Edit">``` donde como se observa se utilizaran directivas ***asp-{...}*** para agregarle propiedades a la vista que solo seran utilizadas para renderizar el html (con los datos correspondientes) a enviar por el servidor. Usar la directiva razor ```asp-for``` tanto en los input como en los labels contenidos en los ```<div class="form-group"></div>``` (***.form-group*** es una clase de bootstrap). Como este es un formulario de edicion dejar los values de cada ```<input/>``` con el valor del atributo de la materia ```@Model.Materia.{Atributo}```. Ya que hay un conjunto finito de planes que pueden ser asociados a una materia es conveniente optar por un elemento ```<select></select>``` que muestre las opciones disponibles (para designar cuales son estas utilizar la directiva ```asp-items="Model.Planes"```, donde ***Model.Planes*** es un campo del viewmodel enviado por el controlador). Finalmente, agregar un input de tipo submit con las clases ```.btn .btn-primary``` de bootstrap. Recordar agregar un campo para preservar el atributo id a ser enviado en la accion nuevamente POST
```html
<input asp-for="Materia.Id" type="hidden" />
```

![image](https://user-images.githubusercontent.com/41701343/132448186-45922162-6000-4913-bf35-46d8c03c91c3.png)

<details close>
<summary>Ver Vista Completa</summary>

```html
@model EditMateriaViewModel

<div class="row">
    <div class="col-lg-4 col-md-8">
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

8. Para utilizar validaciones se utilizara la libreria [FluentValidation](https://docs.fluentvalidation.net/en/latest/aspnet.html), ya que permite realizar validaciones mucho mas complejas y ademas estas resultan mas legibles que cuando se utilizan data annotations. Como primer paso se debe instalar el paquete Nuget ***FluentValidation.AspNetCore*** con el comando ```dotnet add package FluentValidation.AspNetCore``` o utilizando el administrador de paquetes Nuget de Visual Studio. 

9. Luego, en la clase ```Startup``` en el metodo ```ConfigureServices``` (aquel utilizado para inyeccion de dependencias) encadenar el metodo ```.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<MateriaValidator>())``` a ```services.AddControllersWithViews()```. De esta manera se logra la completa integracion de asp net core con esta liberia de validacion, utilizandose exactamente la misma forma de trabajo que con data annotations (el metodo por default del framework). Ademas, para forzar que los mensajes de validacion utilicen las traducciones al español que proporciona la libreria se agregara la siguiente expresion en la funcion que ```.AddFluentValidation(func)``` toma como argumento: ```fv.ValidatorOptions.LanguageManager.Culture = new CultureInfo("es");```

<details close>
<summary>Ver Código</summary>

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews().AddFluentValidation(fv => {
        fv.RegisterValidatorsFromAssemblyContaining<MateriaValidator>();
        fv.ValidatorOptions.LanguageManager.Culture = new CultureInfo("es");
    });
}
```

</details>

10. En la entidad ```Materia``` agregar validaciones para lo siguiente
- La descripcion de una materia debe tener este 3 y 20 caracteres
- Las horas semanales de una materia deben ser entre 2 y 6. Ademas, este campo se debe mostrar como "Horas Semanales" 
- Las horas totales no deben sobrepasar las 150 y tienen que ser superiores a 90 horas. Este campo se debe mostrar como "Horas Totales""
- Los campos Descripcion, HsSemanales, HsTotales y PlanId son requeridos
> Para esto usar la data annotation ```[Display(Name = "nombreX")]``` en la clase Materia y en el constructor de una clase separada ```MateriaValidator``` que herede de la clase generica ```AbstractValidator<Materia>``` agregar las validaciones mediante los metodos ```RuleFor(m => m.AtributoX)``` que son encadenables con ```NotEmpty()```, ```.Length(min:, max:)``` e ```.InclusiveBetween(from:, to:)```

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
    
![image](https://user-images.githubusercontent.com/41701343/132448329-fa3b8ae9-5c2f-4bfd-b201-91555a10fa4e.png)
    
    
<details close>
<summary>Ver Código</summary>

```c#
public class Materia
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    [Display(Name = "Horas Semanales")]
    public int HsSemanales { get; set; }
    [Display(Name = "Horas Totales")]
    public int HsTotales { get; set; }
    public int PlanId { get; set; }
    public Plan? Plan { get; set; }
}

public class MateriaValidator: AbstractValidator<Materia>
{
    public MateriaValidator()
    {
        RuleFor(m => m.Descripcion).NotEmpty().Length(min: 3, max: 20);
        RuleFor(m => m.HsSemanales).NotEmpty().InclusiveBetween(from: 2, to: 6);
        RuleFor(m => m.HsTotales).NotEmpty().InclusiveBetween(from: 90, to: 150);
        RuleFor(m => m.PlanId).NotEmpty();
    }
}
```

</details>

<details close>
<summary>Ver Vista Completa</summary>

```html
@model EditMateriaViewModel

<div class="row">
    <div class="col-lg-4 col-md-8">
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

11. En la accion ```Edit(int id, [Bind("Id, Descripcion, HsSemanales, HsTotales, PlanId")]Materia materia)```, osea la parte que ocurre al hacer submit (POST) del formulario obtenido en el GET de la accion, actualizar la materia del id correspondiente con el metodo ```_materiaRepository.Update(Materia materia)``` y redirigir a la accion ```List```. Recordar comprobar si no se pasaron las validaciones con ```!ModelState.IsValid```, si ocurre re-renderizar la vista devolviendo ```View(new EditMateriaViewModel(...))``` al igual que en el GET y si el id enviado en la ruta no corresponde al enviado en los datos del formulario, si esto ultimo sucede devolver el resultado ```NotFound()```)
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

12. Agregar el codigo para el viewmodel ```CreateMateriaViewModel``` correspondiente a la accion ```Create```. Este es muy similar al viewmodel correspondiente a la accion ```Edit``` con la salvedad que la propiedad ```Materia``` puede ser nula (al hacer el GET de esta accion siempre lo es, ya que es un formulario de creacion por lo que todos sus datos se encuentran vacios), para esto denotar el tipo de referencia nullable ```Materia?``` cuando sea necesario. Ademas, aqui no se preselecciona ninguna ```<option />``` del ```<select />```, ya que esta no es una materia previamente existente y por lo tanto no tiene ningun plan asignado.

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

13. Con la accion GET de la accion ***Create*** tener en cuenta lo mencionado en el paso anterior. En cuanto a la accion POST proceder de la misma manera que con la de ***Edit***, utilizando el metodo ```_materiaRepository.Add(Materia materia)``` y agregando la anotacion ```[Bind("Id, Descripcion, HsSemanales, HsTotales, PlanId")]``` para solo tomar y validar que esten incluidas esas propiedades en la instancia de materia envidada como argumento. Recordar revisar si las validaciones no fueron exitosas.  
    
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

14. Para la vista correspondiente a la accion ***Create*** proceder de forma similar a lo realizado para la vista ***Edit***.
    
![image](https://user-images.githubusercontent.com/41701343/132448388-d1c4d474-e5ac-43b5-9d4d-a4d1365d8d1a.png)  
    
<details close>
<summary>Ver Vista Completa</summary>

```html
<hr />
<div class="row">
    <div class="col-lg-4 col-md-8">
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

15. Ir la carpeta ***/Views/Shared*** vista ***_Layout*** (esta es la master-view donde sobre la que se renderiza cada una de las demas views), alli agregar una opcion mas al navbar ```<li class="nav-item">``` que redirija a la accion ```Create``` mediante las directivas ```asp-area=""```, ```asp-controller="Materia"``` y ```asp-action="Create"```.

<details close>
<summary>Ver Header Completo del Layout</summary>

```html
<header>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
        <div class="container">
            <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">Unidad._5.Lab._1.MVC</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                <ul class="navbar-nav flex-grow-1">
                    <li class="nav-item">
                        <a class="nav-link text-dark" asp-area="" asp-controller="Materia" asp-action="List">Listar Materias</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-dark" asp-area="" asp-controller="Materia" asp-action="Create">Agregar Materia</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</header>
```

</details>

16. En la clase ```Startup``` metodo ```Configure(IApplicationBuilder app, IWebHostEnvironment env)``` en el lugar comentado agregar 
```c#
app.UseStatusCodePagesWithReExecute("/Error/{0}");
```
> Esto permitira redirigir a una pagina de error especificas segun el codigo de error que suceda, el ```{0}``` es remplazado durante la ejecucion por esto ultimo, logrando que se redirija al controlador ```Error``` y la accion marcada por esa ruta. Ejemplo "/Error/404", osea NotFound

17. Ya que no es valido que una accion se llame "404" o "500" se debe utilizar la anotacion ```[Route("...")]``` para no utilizar la convencion de ruta ***"/{controlador}/{accion}/{id?}"*** para esta accion del controlador. Para esto ir al controlador ```Error``` accion ```NotFoundError``` y agregar la anotacion ```[Route("/error/404")]```. Agregar una vista para esta accion, se debe ver como lo siguiente.

![image](https://user-images.githubusercontent.com/41701343/132452274-ee6732fb-6539-491a-b739-f95e07dd9f23.png)
    
<details close>
<summary>Ver Vista Completa</summary>

```html
<div class="page-wrap d-flex flex-row align-items-center">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-12 text-center">
                <span class="display-1 d-block">404</span>
                <div class="mb-4 lead">Pagina no encontrada</div>
                <a asp-area="" asp-controller="Home" asp-action="Index" class="btn btn-link">Back to Home</a>
            </div>
        </div>
    </div>
</div>
```

</details>

18. Tener en cuenta que hay mas tipos de errores, por lo que en la accion ```GenericError(int code)``` agregar la anotacion correspondiente, teniendo en cuenta que en este tipo de anotaciones es posible parametrizar el string de parametro con lo siguiente ```[Route("/error/{code:int}")]```, siendo ***"{code:int}"*** justamente reflejado en el parametro del metodo.
```c#
public IActionResult GenericError(int code)
```

19. En el proyecto ***Test*** clase ```IntegrationTestWeb``` ir a ***Prueba***/***Ejecutar todas las pruebas*** de la barra de herramientas de VS. Esto es para verificar que la implementación cumpla con las especificaciones requeridas. Por ejemplo para la accion ***/Materia/List***:
``` c#
[Fact]
public async Task VisitRootPage_ShouldRenderTwoMateriaCardsAndTheFirstOneMustHaveCertainCardSubtitle()
```
> Cada uno de estos test revisara el html enviado por cada accion de los controladores y chequeara que esten ciertos elementos previamente solicitados, como pueden ser las secciones de validacion o las opciones del select

## Extencion:  Implementar Autenticación y Autorizacion
1. En la clase ```Startup``` ir al metodo ```ConfigureServices(IServiceCollection services)``` y agregar el repositorio de usuarios que servira para registrar usuarios o revisar si estan registrados. Ademas, agregar la clase ```Hasher``` que permitira hashear las contraseñas y obtener un salt, de modo que estas no se guarden en texto plano y la clase ```UsuarioManager``` que agregara la cookie (que servira para identificar al usuario) al navegador del cliente o la eliminara (por lo tanto el usuario ya no podra acceder a los puntos que requieran autenticacion).
```c#
services.AddScoped<IHasher, Hasher>();
services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
services.AddScoped<IUsuarioManager, UsuarioManager>();
```

2. Tambien en ```ConfigureServices(IServiceCollection services)``` agregar y configurar apropiadamente el servicio correspondiente a la autenticacion por cookies mediante:

```c#
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Error/401";
});
```
> ```options.LoginPath``` permitira que cuando el framework redireccione a al usuario despues de intentar acceder a una ruta que requiere autorizacion este lo haga a la url que se tiene implementada. Esto es especialmente necesario para ```options.AccessDeniedPath``` ya que el fw por default envia a los usuarios que estan autenticados pero que no cuentan con el rol o permiso requerido para acceder a una ruta determinada a ***/Account/AccessDenied*** (ruta que no esta implementada en esta aplicacion), sin embargo si esta implementada la ruta ***/Error/401*** para estos casos.

3. En el metodo ```void Configure(IApplicationBuilder app, IWebHostEnvironment env)``` agregar el middleware ```app.UseAuthentication()``` exactamente despues que el otro middleware ```app.UseRouting()``` y antes que ```app.UseAuthorization()```. Esto es debido a que la request pasa por los middlewares exactamente en el orden que estan declarados en este metodo.

4. En la clase ```UsuarioRepository``` metodo ```UsuarioLogeado? Validar(LoginViewModel loginVM)``` completar el predicado que sirve como argumento de ```_usuarios.Find(u => completar)``` sabiendo que se quiere obtener el usuario para el cual su mail (u.Mail) coincida con el enviado como parametro en el login view model y cuyo hash de contraseña (```u.Clave```) sea equivalente al hash generado gracias a la clave enviada en el loginVM y el salt del usuario guardado (```_hasher.GenerateHash(loginVM.Clave, u.Salt)```)

<details close>
<summary>Ver Codigo</summary>

```c#
var user = _usuarios.Find(u => u.Mail == loginVM.Mail && _hasher.GenerateHash(loginVM.Clave, u.Salt) == u.Clave);
```

</details>

5. En la clase ```UsuarioRepository``` metodo ```UsuarioLogeado? Register(RegisterViewModel registerVM)``` completar el seteo del usuario a guardar mediante los datos enviados en el register view model, teniendo en cuenta que la contraseña no debe ser guardada en formato texto plano, es decir es requerido que esta se le realice un proceso de hashing y este hash ademas de su salt asociado sean guardados para cualquier usuario.

<details close>
<summary>Ver Codigo</summary>

```c#
var salt = _hasher.GenerateSalt();
var hashedPassword = _hasher.GenerateHash(registerVM.Clave, salt);

Usuario user = new()
{
    Id = _usuarios.Count() + 2,
    Nombre = registerVM.Nombre,
    Mail = registerVM.Mail,
    Clave = hashedPassword,
    Salt = salt
};
```

</details>

6. Ir a la clase ```UsuarioManager``` metodo ```async Task SignIn(HttpContext httpContext, UsuarioLogeado usuarioLogeado, bool isPersistent = false)```. Es notable que este metodo cuenta con una caracteristica que no fue requerida hasta ahora en la materia, el manejo asincronico. Esto ultimo significa que este liberara el hilo de ejecucion mientras espera (```await```) que la tarea se complete (```Task<T>```, donde T no esta presente aqui ya que no se espera que la tarea contenga algun dato a devolver, es decir solo se espera que la tarea devuelta se complete en algun momento). Una vez entendido eso, declarar una lista de objetos ```Claim``` (declaraciones que son emitidas por el servidor y contienen ciertos datos que se van a considerar como validos si se comprueba que el generador de estos fue este) tomando como ejemplo que el primer elemento sera ```new Claim(ClaimTypes.NameIdentifier, usuarioLogeado.Id.ToString())```, agregar claims para ```ClaimTypes.Name```, ```ClaimTypes.Email``` y ```ClaimTypes.Role```. 

<details close>
<summary>Ver Codigo</summary>

```c#
var claims = new List<Claim>()
{
    new(ClaimTypes.NameIdentifier, usuarioLogeado.Id.ToString()),
    new(ClaimTypes.Name, usuarioLogeado.Nombre),
    new(ClaimTypes.Email, usuarioLogeado.Mail),
    new(ClaimTypes.Role, usuarioLogeado.Role.ToString())
};
```

</details>

7. Luego, crear una instancia de ```ClaimsIdentity``` suministrando el listado de claims y el esquema de autenticacion ```CookieAuthenticationDefaults.AuthenticationScheme```. 

```c#
string authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, authScheme));
```

8. Finalmente, hacer el await del metodo asincronico ```httpContext.SignInAsync(...)``` indicando como parametros el esquema de autenticacion, el claimPrincipal y si la cookie debe persistir entre sesiones (es decir cuando se cierra y abre nuevamente el navegador)

```c#
await httpContext.SignInAsync(authScheme, claimPrincipal, new AuthenticationProperties() { IsPersistent = isPersistent });
```

9. En el metodo ```async Task SignOut(HttpContext httpContext)``` unicamente se debe esperar al metodo asincronico ```httpContext.SignOutAsync(...)``` que toma como parametros el mismo esquema de autenticacion que el utilizado en el paso anterior

<details close>
<summary>Ver Codigo</summary>

```c#
await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
```

</details>

10. En el controlador ```Account``` metodo ```async Task<IActionResult> Login(LoginViewModel loginVM)``` (notar que este tambien es asincronico ya que requiere esperar al metodo ```async Task SignIn(...)``` de ```UsuarioManager```, recordar que al hacer esto el hilo de ejecucion se libera volviendo al pool de threads). Alli, llamar al metodo ```_usuarioRepository.Validar(LoginViewModel)``` y chequear si el usuario que devuelve es null, en el caso de que suceda agregar al ModelState un error con ```ModelState.AddModelError(key, value)``` donde la key deberia ser ***""***, ya que es error que no esta asociado a ninguna campo del formulario en particular sino que es mas bien general (por lo que se mostrara arriba en la seccion ```<div asp-validation-summary="ModelOnly" class="text-danger"></div>``` de la vista)

<details close>
<summary>Ver Codigo</summary>

```c#
var loggedUser = _usuarioRepository.Validar(loginVM);

if (loggedUser == null)
{
    ModelState.AddModelError("", "Mail o contraseña incorrectos");
    return View(loginVM);
}
```

</details>

11. A continuacion, esperar al metodo ```_usuarioManager.SignIn(HttpContext httpContext, UsuarioLogeado usuarioLogeado, bool isPersistent)``` suministrando el contexto de la request en curso mediante ```this.HttpContext``` y los datos obtenidos del metodo anterior ademas de la preferencia sobre mantener la sesion abierta seteada en el login view model. Finalmente, siendo lo anterior exitoso, redirigir a la accion ```Index``` del controlador ```Materia```

<details close>
<summary>Ver Codigo</summary>

```c#
await _usuarioManager.SignIn(this.HttpContext, loggedUser, loginVM.IsPersistent);

return RedirectToAction(controllerName: "Materia", actionName: "Index");
```

</details>

12. Ir a la clase ```RegisterValidator``` en el archivo ***/Model/RegisterViewModel***, alli agregar validaciones para lo siguiente:
- El nombre de un usuario debe tener este 3 y 30 caracteres
- El mail debe tener el formato apropiado.
- La clave debe ser de mas de 6 caracteres, con uno en mayuscula y al menos un numero (usar el metodo ```.Must(p => p``` de ***FluentValidator*** junto con el ```.Any(Char.IsDeterminadoTipo)``` de ***LINQ*** para strings)
- El campo confirmarClave debe coincidir con lo ingresado para el campo clave (usar el metodo ```.Equal(m => m.Atributo)``` de ***FluentValidation***). Debe mostrarse como "Confirmar Clave"

![imagen](https://user-images.githubusercontent.com/41701343/133905735-cab915ed-48f7-42ff-8020-3ead6928fe4b.png)
    
<details close>
<summary>Ver Codigo</summary>

```c#
public record RegisterViewModel
{
    public string Nombre { get; set; }
    public string Mail { get; init; }
    public string Clave { get; init; }
    [Display(Name = "Confirmar Clave")]
    public string ConfirmarClave { get; init; }
    public bool IsPersistent { get; init; }
}

public class RegisterValidator: AbstractValidator<RegisterViewModel>
{
    public RegisterValidator()
    {
        RuleFor(rvm => rvm.Nombre).Length(min: 3, max: 30);
        RuleFor(rvm => rvm.Mail).NotEmpty().EmailAddress();
        RuleFor(rvm => rvm.Clave).NotEmpty().MinimumLength(6)
            // No se utiliza .Matches(@"\w*[A-Z]+\w*") ya que esto solo reconoce caracteres ASCII,
            // es decir da errores de validacion al usar caracteres Unicode como 'Ñ' o 'ñ'
            .Must(p => p.Any(Char.IsUpper)).WithMessage("La contraseña debe tener al menos un caracter en mayuscula")
            .Must(p => p.Any(Char.IsNumber)).WithMessage("La contraseña debe tener al menos un numero");
        RuleFor(rvm => rvm.ConfirmarClave).Equal(rvm => rvm.Clave);
    }
}
```

</details>

13. En el caso de accion ```async Task<IActionResult> Register(RegisterViewModel registerVM)``` del controlador ```Account``` proceder de la misma manera, teniendo en cuenta que en este caso si loggedUser es devuelto como nulo por ```_usuarioRepository.Register(registerViewModel)``` es debido a que ya hay un usuario registrado con esos datos.

<details close>
<summary>Ver Codigo</summary>

```c#
var loggedUser = _usuarioRepository.Register(registerVM);

if (loggedUser == null)
{
    ModelState.AddModelError("", "Usuario ya registrado");
    return View(registerVM);
}

await _usuarioManager.SignIn(this.HttpContext, loggedUser, registerVM.IsPersistent);

return RedirectToAction(controllerName: "Materia", actionName: "Index");
```

</details>

14. Para la accion ```async Task<IActionResult> Logout()``` esperar al metodo ```_usuarioManager.SignOut(HttpContext)```,
proveyendo como argumento el contexto de la request actual de la misma forma que se vio anteriormente (recordar que es una propiedad que es heredada de la clase padre ```Controller```). Por ultimo, redirigir de forma permanente a la accion ```Index``` del controlador ```Home```

<details close>
<summary>Ver Codigo</summary>

```c#
await _usuarioManager.SignOut(this.HttpContext);

return RedirectToActionPermanent(controllerName: "Home", actionName: "Index");  
```

</details>

15. Ir a la accion ```Create``` del controlador ```Materia``` y agregar la anotacion ```[Authorize]```, que permitira redireccionar las request que no cuenten con una cookie valida que identifique que usuario la esta realizando. Ademas, en la 
 accion ```Edit``` agregar ```[Authorize(Roles = "Admin")]```, esto permitira no dar acceso a aquellos que no esten logeados y ademas rechazar a los que no cuenten con el rol admin alojado como claim en la cookie (en este caso no redirigiendo al login obviamente, sino enviandolos a la pagina de error ***NotAuthorized***).

```c#
[Authorize(Roles = "Admin")]
public IActionResult Edit(int? id)

[Authorize(Roles = "Admin")]
public IActionResult Edit(int id, [Bind("Id, Descripcion, HsSemanales, HsTotales, PlanId")]Materia materia)

[Authorize]
public IActionResult Create()

[Authorize]
public IActionResult Create(Materia materia)
```

16. En cuanto a la pagina de error ***NotAuthorized*** esta esta asociada a la accion ```NotAuthorized``` del controlador ```Error```. Se solicita programar una vista que se vea de la siguiente manera:

> Como novedad aqui se utilizara un icono de la libreria bootstrap icons (```<i class="bi bi-shield-fill-exclamation display-1 d-block"></i>```), para lo cual se debe incluir su cdn en el ```<head></head>``` de la vista ```Shared/_Layout``` (```<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css">```)

![imagen](https://user-images.githubusercontent.com/41701343/133905728-612bf984-15d5-40a3-834f-5d127abfaae1.png)   
    
<details close>
<summary>Ver Vista completa</summary>

```html
<div class="page-wrap d-flex flex-row align-items-center">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-12 text-center">
                <i class="bi bi-shield-fill-exclamation display-1 d-block"></i>
                <div class="lead">No</div>
                <div class="mb-4 lead">Autorizado</div>
                <a asp-area="" asp-controller="Home" asp-action="Index" class="btn btn-link">Back to Home</a>
            </div>
        </div>
    </div>
</div>
```

</details>

17. En la vista maestra ```Shared/_Layout``` renderizar segun si el usuario ***no*** este logeado los nav items correspondientes a Login y Register. Esto es posible realizarlo gracias al uso de la directiva razor ```@if (User.Identity?.IsAuthenticated == false) { ... }```. Mostrar ambos items a la derecha del nav-bar.

![imagen](https://user-images.githubusercontent.com/41701343/133905619-6f6fd86c-952e-4545-8abd-a855a987fef6.png)    
    
<details close>
<summary>Ver Vista Parcial</summary>

```html 
@if (User.Identity?.IsAuthenticated == false)
{
<li class="ml-md-auto"></li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Account" asp-action="Login">Login</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Account" asp-action="Register">Register</a>
</li>
}
```

</details>

18. Tambien en la vista maestra ```Shared/_Layout``` renderizar segun si el usuario ***si*** este logeado un ```dropdown-menu``` (de bootstrap) con el email como ```dropdown-toggle``` y el nombre junto con la opcion de Logout como ```dropdown-items```. Utilizar la directiva razor ```@User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.TipoApropiado)?.Value```, que permite obtener y filtrar el listado de claims (en este caso ```ClaimTypes.Email``` y ```ClaimTypes.Name```) de la cookie que pertenece a la request actual.

![imagen](https://user-images.githubusercontent.com/41701343/133905596-b325e35a-25ad-403e-a875-068b7e5b2f9f.png)    
    
<details close>
<summary>Ver Vista Parcial</summary>

```html
@if (User.Identity?.IsAuthenticated == true)
{
<li class="ml-md-auto"></li>
<li class="nav-item dropdown">
    <a class="nav-link dropdown-toggle ml-auto" href="#" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
        @User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value
    </a>
    <div class="dropdown-menu">
        <div class="dropdown-item dropdown">
            @User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Name)?.Value
        </div>
        <div class="dropdown-item">
            <a class="nav-link text-dark" asp-area="" asp-controller="Account" asp-action="Logout">Logout</a>
        </div>
    </div>
</li>
}
```

</details>
