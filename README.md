# Labs-2021

En este repositorio se encuentran todos los laboratorios utilizados en la asignatura Tecnologías de Desarrollo de Software IDE, materia electiva de la UTN FRRo.

|Índice|
|:-|
|:computer: [Pre-Requisitos](#Pre-Requisitos)|
|:bookmark_tabs: [Glosario](#Glosario)|
|:briefcase: [Instrucciones Git](#Instrucciones)|

## Pre-Requisitos

|Requisito|Descripción|Link|
|:-|:-|:-|
|Visual Studio .Net|Entorno para programar. Es deseable tener descargada la última versión|[https://visualstudio.microsoft.com/es/downloads/](https://visualstudio.microsoft.com/es/downloads/)|
|Usuario GitHub|Repositorios online|[https://github.com/join](https://github.com/join?ref_cta=Sign+up&ref_loc=header+logged+out&ref_page=%2F&source=header-home)|
|GitKraken|Control de versiones con repositorios Git locales|https://www.gitkraken.com/download|

> Nota: La versión gratuita de GitKraken posee todo lo necesario a utilizar, pero existe para el que le guste la herramienta una versión premium con otras features como: Tener Boards estilo trello y calendarios, entre otras cosas. Se puede conseguir gratuitamente como alumno universitario, teniendo un correo @frro dado de alta en la universidad (Secretaría de Asuntos Universitarios).

## Glosario

### Repositorio
> Almacenamiento virtual de tu proyecto. Te permite guardar versiones del código a las que puedes acceder cuando lo necesites.

### Control de Versiones
> Gestión de los cambios que se le realizan al código, puede ser manual (utilizando prefijos vN.M y guardando los archivos iterativamente en el equipo o en Drive por ejemplo) o utilizando alguna herramienta que lo facilite (Git en este caso).

### Git
> Sistema de control de versiones distribuido, esto significa que sirve para gestionar los cambios en el código de forma local y en un repositorio remoto que es ejecutado en un servidor central. Es gratis y opensource. Web para descargar: [https://git-scm.com/](https://git-scm.com/)

### Fork
> Generar un repositorio bifurcado que está asociado tanto al repositorio original como al propio. Permite probar cambios que no son deseables de hacer en el original y a la vez mantenerse al día con los cambios que puedan surgir allí

### Clone
> Equivalente a descargar los binarios del repositorio. Se debe copiar la dirección URI del repositorio que se quiere clonar. Al usar esta herramienta  se optimiza la descarga de código y a la vez mejora la usabilidad. Un ejemplo sería: encuentro un repositorio interesante en GitHub y deseo tenerlo localmente en mi computadora, en vez de descargar el repo manualmente, lo clono desde el remoto a través de su dirección URI y se genera el repo local en mi computadora directamente.

### Staging Area
> Área donde se congelan los cambios realizados en los archivos que se agregaron a está. Son todos los cambios que tenemos de forma local pero que aún no han sido incluídos en un "commit" y por lo tanto no podemos enviarlos aún a nuestro repositorio remoto.

### Branch
> Cuando quieres realizar cambios es recomendable realizarlos en una “sección propia” o Branch bifurcada desde algún punto de partida (desde un commit específico) a donde subirás los cambios y podrán ser vistos en el control de versiones online como GitHub o GitLab, pero estos cambios no se ven reflejados en el Master de tu repositorio directamente, hasta que se realice un Merge de los cambios que tienes en tu Branch hacia Main. (Ejemplo: tengo que hacer una feature llamada “Login” para el usuario, creo una rama “login_user” y empiezo a subir mis cambios ahí. Cuando termino la feature, “mergeo” la Branch login_user a Master y de esta forma todos nuestros cambios serán enviados al Main del proyecto). Puedes cambiar de rama con el comando '''git checkout nombreBranch'''

### Commit
> Información que contiene los nuevos cambios que realicé en el código, quien lo realizó, un ID y la fecha. Al realizar commits, podemos subirlos de nuestro repo local al repositorio remoto compartido con el equipo, mediante ‘’’git push origin -u nombreBranch’’’. También podemos traernos los Commits que realizaron otros miembros del equipo y subieron al remoto para mantenernos actualizados con todos los cambios, mediante ‘’’git pull’’’. Tener en cuenta que Push y Pull funciona por Branch, por lo que si se quiere es traer o enviar Commits a otras Branches, se debe cambiar de Branch haciendo ‘’’git checkout nombreBranch’’’

### Pull
> Descargar los cambios presentes en repositorio [remote] a local. Todos los commits subidos al remoto por por los demás integrantes del grupo serán descargados en tu repo local para mantenerte sincronizado. Solo trae los cambios que detecta en la rama actual.

### Push
> Subir los cambios realizados en local al repositorio [remote](#Remote). Solo sube todos los cambios que se encuentren dentro de Commits. Los demás integrantes del equipo podrán descargar tus cambios y mantenerse sincronizados. Solo sube al remoto los commits que detecta en la rama actual.

### Merge
> Agregar los cambios presentes en una branch determinada a otra branch. Ejemplo: realizé 3 commits en una branch llamada "register_user" y ahora quiero que estos commits estén en master porque completé la tarea y ya no trabajaré en esa branch, entonces se "mergean" los commits de register_user en Master.

### Pull Request
> Se podria llamar “Merge Request” que es más exacto (En GitLab posee ese nombre). Es una solicitud que realiza un integrante del equipo hacia los demás, pidiendo agregar los cambios realizados en una determinada branch a otra. Esto se gestiona por determinados usuarios revisores que pueden optar por aprobar lo realizado, hacer comentarios para requerir cambios o preguntas para aclarar porqué se procedió de determinada manera. Los comentarios se realizan online en Github y pueden ser comentarios generales, por archivo o por linea. Una vez que los reviewers aprueban los cambios, se acepta la Pull Request y los commits nuevos se mergean (por default) a master.

### [Remote]
> Refiere al repositorio central que es ejecutado en un servidor con determinado nivel acceso para cada uno de los desarrolladores. Es el repositorio que almacena github y al cual se le realiza Push para enviar commits y Pull para traer commits de los demás integrantes del equipo.

### Origin
> Es el repositorio local manejado por cada desarrollador en sus equipos. Se comunica con el repositorio remoto alojado en GitHub mediante un area intermediaria a la cual denominaremos "Staging Area", en donde enviaremos nuestros commits con Push, y recibiremos commits de los demas integrantes del equipo que hayan subido sus commits con Pull.

### Master
> Branch principal del repositorio, en esta generalmente se cuenta con la versión más estable del código. Una forma recomendada de trabajar es hacer una branch por cada nuevo requerimiento o caso de uso que estemos trabajando y subamos commits allí, y luego de terminarlo recién ahí pasarlos a "Master" (mediante un Merge directo o una Pull Request en donde los otros integrantes deben aprobar los cambios antes).
> Nota: desde 2020 en Git se reemplazó la branch Master por “Main”. Solamente los repositorios creados posteriormente a 2020 poseen este cambio.

### Commits Log
> Historial de commits. Cada repositorio tiene uno, y te cuenta "la historia" de los commits con autores, cambios y fechas. Todos los cambios que hagamos en el repo son guardados automaticamente en el log. Puede accederse a él mediante '''git log'''.

### Commits Tree
> Ver el Historial de commits de nuestro repo (Commit Log) como un árbol con sus ramificaciones (Branches). GitKraken presenta un Tree en la vista principal del repositorio.

### HEAD
Posición en la que nos encontramos en el Commit Tree. Por default siempre que hagamos pull estaremos viendo hasta el último de los commits, pero podemos "volver atrás" versiones de nuestro código moviendo el "HEAD" hacia otro punto de la historia (ejemplo: quiero ver como era el codigo hace una semana, y se hicieron 5 commits esta semana, entonces vuelvo 5 commits atrás y veré esa versión antigua de mi código sin los cambios nuevos). Mover el head no borra ni crea commits nuevos, no es peligroso.

### Status
Ver el estado general de mi repositorio remoto en la branch actual, con respecto a remoto. Me dirá en que branch estoy, cuantos commits tengo por enviar, cuantos cambios tengo en "Staging Area" sin aun haberlos metido en un commit nuevo, etc... GitKraken ofrece esta información de forma visual en la vista principal del repositorio, pero por consola se debería escribir '''git status'''.

### Reset
Si queremos deshacer cambios locales que aún no hemos enviado al remoto, podemos realizar un "reset" de nuestros cambios. El reset puede revertir uno o más commmits locales. En Gitkraken se hace un reset tocando el ícono de la papelera al lado de nuestros cambios. Hay 3 tipos de reset. El '''git reset --soft''' que solamente mueve "HEAD" hacia atrás sin borrar los cambios, es solo para ver nuestro codigo en una version anterior antes de los cambios; el '''git reset --mixed''' (el default) que borrará la cantidad de commits que le indiquemos, pero los cambios no se pierden sino que se vuelven al "Staging Area" y nos dejará hacer nuevos commits; y el '''git reset --hard''' que es peligroso pues borra directamente la cantidad de commits solicitados y estos se "pierden para siempre".

### Stash
Si tenemos cambios sin commitear y cambiamos de branch, podemos "guardarlos" temporalmente en un "vault" llamado Stash. Más adelante si los necesitamos de vuelta en la misma branch u otra, se realiza un Stash Pop que recupera el código y lo coloca devuelta en Staging Area.

### Rebase
Un rebase modifica la historia de commits de nuestro repositorio, por lo tanto es peligroso. Podemos necesitarlo por ejemplo cuando creamos commits incorrectos y queremos cambiarlos, pero estos ya los subimos al remoto con Push y nos arrepentimos. Con rebase se puede cambiar el nombre de commits en remoto, unificar diferentes commits en uno, borrar commits enteros, entre otras cosas. Estando en la branch en la que queremos modificar (se recomienda antes crear otra branch desde el ultimo commit para tener una "copia de seguridad") se debe escribir '''git rebase -i HEAD~n''', donde n se debe reemplazar por la cantidad de commits que se van a ver afectados por el rebase, y la -i nos indica que será un rebase interactivo (te da instrucciones de que se puede hacer con cada commit, sea unificar, renobrar, borrar, etc...).

> Para una explicación mucho mas completa consultar: [https://elc.github.io/posts/git-guide-with-visual-interface/es/](https://elc.github.io/posts/git-guide-with-visual-interface/es/)
de Ezequiel Castaño, alumno avanzando de sistemas UTN FRRo [https://github.com/ELC](https://github.com/ELC)

## Instrucciones Git
La forma de trabajo que se va a utilizar este año consiste en los siguientes pasos:
> Esta se encuentra inspirada por la forma de trabajo que es utilizada en otra materia electiva de la UTN, y fue implementada por [@ELC](https://github.com/ELC) en aquella materia.

1. Forkear el repositorio oficial de practica seleccionando su usuario de GitHub
![tutoGit_P0](https://user-images.githubusercontent.com/41701343/111100579-d2fd5000-8526-11eb-83a3-119c0b2ce18e.png)

2. Clonar el repo forkeado
- Primero copiar al portapapeles la dirección url de este
![tutoGit_P2](https://user-images.githubusercontent.com/41701343/111100784-4606c680-8527-11eb-8e96-e00ed107bb12.png)
- Luego acceder a GitKraken y copiar la url anterior en el formulario de clonacion
![tutoGit_P3](https://user-images.githubusercontent.com/41701343/111100928-9aaa4180-8527-11eb-8d9b-548e5f562a36.png)
![tutoGit_P4](https://user-images.githubusercontent.com/41701343/111100985-ba416a00-8527-11eb-9099-341edb9c4c60.png)

3. Abrir la carpeta con Visual Studio desde el menú contextual (click derecho)
![tutoGit_P6](https://user-images.githubusercontent.com/41701343/111101096-f674ca80-8527-11eb-9518-2e316b3c6568.png)

4. Cambiar la ubicación predeterminada de los proyectos en Visual Studio
- Ingresar "ubic" en la *Barra de Búsqueda*
![tutoGit_P7](https://user-images.githubusercontent.com/41701343/111101162-16a48980-8528-11eb-8f06-77e28c36a81b.png)
- Cambiar la ubicación predeterminada a la dirección donde esta el repositorio clonado previamente
![tutoGit_P8](https://user-images.githubusercontent.com/41701343/111101168-1906e380-8528-11eb-928b-78e6c17394dc.png)

5. Ir a al menú desplegable en *Archivo*/*Nuevo*/*Proyecto*
![tutoGit_P9](https://user-images.githubusercontent.com/41701343/111101334-6f742200-8528-11eb-886f-e33286c1c9e5.png)

6. Llenar el nombre del laboratorio en el campo *Nombre del Proyecto*, el campo *Nombre de la Solución* se ingresara automáticamente con el mismo valor. 
- Luego clickear en el selector de archivos correspondiente al campo *Ubicación*
![tutoGit_P10](https://user-images.githubusercontent.com/41701343/111101400-90d50e00-8528-11eb-9f25-9d2ea5fa675f.png)
- Ingresar la subcarpeta correspondiente a la unidad y el laboratorio en el que se quiera trabajar
![tutoGit_P11](https://user-images.githubusercontent.com/41701343/111101521-dc87b780-8528-11eb-9c76-e42978b920b3.png)

7. Seleccionar NET 5 en el menú de opciones

8. Notar como los nuevos archivos se incluyen en el staging area (sección donde están los archivos marcados) y clickear en stage all changes
![tutoGit_P13](https://user-images.githubusercontent.com/41701343/111101580-faedb300-8528-11eb-88b8-d592ecd8b8f0.png)

9. Crear una nueva branch con el siguiente formato: UnidadNLabZ con N y Z igual al numero de unidad y de laboratorio respectivamente
![tutoGit_P14](https://user-images.githubusercontent.com/41701343/111101723-41dba880-8529-11eb-97df-46b6864b6fcf.png)

10. [Este paso se repite] Commitear los cambios escribiendo un mensaje que represente lo realizado, como por ejemplo la inicializacion del proyecto con Visual Studio
![tutoGit_P15](https://user-images.githubusercontent.com/41701343/111101922-a991f380-8529-11eb-9119-cef1a468d9e4.png)

11. Observar como los commits en la branch "master" están tanto en local (símbolo notebook) como en remoto (icono del usuario de GitHub). Para cambiar esto pushear los cambios a remoto con el siguiente botón
![tutoGit_P16](https://user-images.githubusercontent.com/41701343/111102098-0e4d4e00-852a-11eb-8c0c-8e009a2a0883.png)

12. Agregar el repositorio de la asignatura a remote, de manera de que pueda tener los ultimas actualizaciones a los ejercicios y sea capaz de realizar pull request a este
![tutoGit_P17](https://user-images.githubusercontent.com/41701343/111102253-697f4080-852a-11eb-8c81-a59aed316325.png)
