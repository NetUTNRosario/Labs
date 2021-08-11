# Unidad 2 � Capitulo 3 - Laboratorio 1

### Objetivos
Familiarizarse con el uso de LINQ to Objects

### Duraci�n Aproximada
60 minutos

### Pasos
1. Completar la implementaci�n en la funci�n correspondiente de la clase ***FuncionesLinq***
2. En la clase ***FuncionesLinqTest*** ir a ***Prueba***/***Ejecutar todas las pruebas*** de la barra de herramientas de VS. Esto es para verificar que la implementaci�n cumpla con las especificaciones requeridas

> Si los tests automatizados en ***FuncionesLinqTest*** pasan no es necesario llamar a todas las funciones por consola, esto representa la t�pica practica de testing manual

### Ejercicios
1. Dado un Array que incluya todas las Provincias de Argentina usar LINQ para obtener y mostrar
por Consola aquellas provincias que empiezan con la letra �S� o �T�

2. Crear un programa que acepte una lista de n�meros, los almacene en un objeto del tipo List&lt;int&gt;
y luego usando LINQ muestre por Consola aquellos que son mayores a 20.


3. Dado un ArrayList que incluya al menos 10 Ciudades de Argentina incluyendo Nombre y C�digo
Postal usar LINQ para obtener y mostrar por Consola el C�digo Postal de aquellas ciudades que
incluyan dentro de su nombre una expresi�n de b�squeda de tres caracteres, sin respetar
may�sculas o min�sculas. Por ejemplo, si se ingresa �ros� y el ArrayList incluye Rosario entonces
debe mostrarse el C�digo Postal de Rosario.


4. Dada una List&lt;Empleado&gt; donde Empleado tiene las propiedades Id (int), Nombre (string),
Sueldo (decimal). Crear un programa que acepte dar de alta Empleados en esta lista y luego
muestre por Consola esta misma Lista ordenada por la propiedad Sueldo, tanto de manera
ascendente como descendente.