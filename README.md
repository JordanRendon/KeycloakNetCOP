Configuración para integrar Keycloak 

Comando para crear contenedor en Keycloak
docker run -p 8080:8080 --name keycloak -e KEYCLOAK_USER=admin -e KEYCLOAK_PASSWORD=admin quay.io/keycloak/keycloak:latest

una vez se corra el contenedor podemos ingresar a la url http://localhost:8080/ donde ingresaremos el user:admin y password:admin para ingresar al sitio y configurarlo 
![image](https://github.com/user-attachments/assets/9a5ba3e8-786b-4a41-ba90-ff7005c938e5)
 
una vez ingresemos lo primero que se hará es crear un Realm para el espacio de trabajo donde se pondrá gestionar los usuario, clientes, roles y permisos.
![image](https://github.com/user-attachments/assets/fed8e859-cc13-44f8-90aa-393e3e73d9a2)

En esta parte solo es agregar el nombre del realm, en nuestro caso lo creamos como demo-realm
 ![image](https://github.com/user-attachments/assets/2ccccf2d-1115-41bb-b91e-1a54a7b0311d)

seleccionamos el realm creado y ingresamos a Clients, para luego crear un nuevo client
 ![image](https://github.com/user-attachments/assets/d2354ce0-ce6d-49b1-8be0-2d4870ba14e5)

ingresamos el nombre del client y también el clien ID el cual utilizaremos para su conexión
![image](https://github.com/user-attachments/assets/d16a8f59-9e97-4eaa-a52b-c1e1c0c93f55)
 
damos en next y habilitamos la opción Client authenticatio en On, habilitamos las casillas Direct Access grant y Service accounts roles
 ![image](https://github.com/user-attachments/assets/97222b39-7710-4cfb-815b-c9a9ce8bc8a6)


damos en next y en esta parte lo dejamos tal cual esta y daremos click en Save
 ![image](https://github.com/user-attachments/assets/b48571e4-428d-4da0-9cb9-19a3987b018a)


una vez creado podemos ingresar a nuestro client para asi obtener nuestro client secret
 ![image](https://github.com/user-attachments/assets/780bca60-34fc-4bbe-98b7-4ec391f07262)

nos dirigimos a la pestaña credentials y aquí podemos copiar el client sercret para la conexión 
 ![image](https://github.com/user-attachments/assets/fabafbf7-8af3-43ff-8385-5a0baaef135a)



ahora configuramos la parte del Audience para su mapeo para que  tanto account, como client sean validos en el token 
nos dirigimos a Client Scope y damos en Create client scope

![image](https://github.com/user-attachments/assets/2fe7cf1f-3a20-40f2-8a43-871f424bf0c5)

Agregamos el nombre , una descripción, type lo dejamos por default, Display on consent screen en On, luego daremos en guardar 
![image](https://github.com/user-attachments/assets/beee79ff-d816-41ec-b287-115f04272860)


Despues de guardar nos dirigimos a la pestaña Mapper y le daremos en Add mapper 
![image](https://github.com/user-attachments/assets/a9b844ee-394d-4ba1-86c0-6f6bf6a18232)


en esta parte elegimos la opción Audience
![image](https://github.com/user-attachments/assets/86670fe4-4c18-43ca-8d15-d736ed7c89ce)


aquí agregamos el nombre, el included client audience el cual será client(ClientID), marcamos Add to ID token en On y dejamos el resto tal cual esta , y daremos en guardas
![image](https://github.com/user-attachments/assets/13ea65cc-e3ba-4b7b-b5c6-920be1e43c9a)


Volvemos a Clients , luego a client, le daremos en la pestaña Client Scope y lugo en Add client scope para agregar el nuevo Audience que creamos 
![image](https://github.com/user-attachments/assets/fdcefbb4-ee69-4aa1-81d3-0b6c22c812da)


seleccionamos el Audience (custom-audience), damos es Add y luego en Defaul
![image](https://github.com/user-attachments/assets/14c3c390-633d-4bb9-9e3b-eca16f8cd042)


ahora vamos a configurar el rol para tener permisos de roles en nuestros servicios 

nos dirigimos a Realm roles y daremos click en Create rol
![image](https://github.com/user-attachments/assets/a9c8974b-477c-4f99-816b-4914f81e46de)
 

agregamos el nombre del rol y damos en guardar(save)
![image](https://github.com/user-attachments/assets/325c9ded-7f76-4925-8322-370aaca6df65)


una vez creado el rol nos dirigimos a los Users y ingresamos a testuser que creamos anteriormente para asignar el rol creado
![image](https://github.com/user-attachments/assets/67b54275-44dc-4841-937d-63f5ba0121eb)
 

Aquí vamos a la pestaña de Role mapping y luego daremos click en Assign role
![image](https://github.com/user-attachments/assets/eb481086-2e02-494f-a65b-e1386d2888b1)


En esta ventana le daremos en Filter by clients y luego en Filter by real roles
![image](https://github.com/user-attachments/assets/aff42b21-be33-4d88-82c9-f1187ab47c71)
 

Luego en la siguiente ventana seleccionamos el rol creado “user” y damos en Assing
![image](https://github.com/user-attachments/assets/6fcaf568-a269-43ac-81db-c05004e51ea1)


con estas configuraciones ya podemos utilizar keycloak para generar el token de la autenticación, proteger los servicios con autenticación y permisos con roles. 
