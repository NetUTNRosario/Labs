# practica-2021

En el presente repositorio se encuentran todos los laboratorios que se van a utilizar en la asignatura Tecnologías de Desarrollo de Software IDE, materia electiva de la UTN FRRo.

#### Pre-Requisitos
- Visual Studio .Net (deseable última versión)
> Programación
>
> https://visualstudio.microsoft.com/es/downloads/
- Usuario GitHub
> Repositorios online
> 
> https://github.com/join?ref_cta=Sign+up&ref_loc=header+logged+out&ref_page=%2F&source=header-home
- GitKraken
> Control de versiones con repositorios Git locales
> 
> https://www.gitkraken.com/download

#### Glosario (en términos simples)

> Para una explicación mucho mas completa consultar: https://elc.github.io/posts/git-guide-with-visual-interface/es/
de Ezequiel Castaño, alumno avanzando de sistemas UTN FRRo https://github.com/ELC

### Instrucciones
La forma de trabajo que se va a utilizar este año consiste en los siguientes pasos:
> Esta se encuentra inspirada por la que es utilizada en la materias electiva Soporte a la Gestión de Datos con Programación Visual (Python)  
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
