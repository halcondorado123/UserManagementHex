namespace APIUserValidation.Helpers
{
    public static class SwaggerComments
    {
        public static class Clients
        {
            public const string GetUsersHeader = "Obtiene una lista de todos los usuarios de la base de datos. [SQL Vista]";
            public const string GetUsersDescription = @"Este endpoint permite consultar todos los usuarios registrados en el sistema."
                                                            + "La respuesta incluirá el ID del usuario, nombre completo, número de teléfono y correo electrónico.\n\n"
                                                            + "Si no hay usuarios registrados, se retornará una lista vacía.";

            public const string GetUsersByIdHeader = "Obtiene un usuario por su ID. [SQL Function]";
            public const string GetUsersByIdDescription = @" Este endpoint permite consultar los detalles de un usuario específico utilizando su ID.";

            public const string CreateUserHeader = "Crea un nuevo cliente [SQL Stored Procedure]";
            public const string CreateUserDescription = "Este endpoint permite crear un nuevo cliente con los datos proporcionados, como nombre, teléfono y correo electrónico. Si alguno de los campos está vacío o nulo, se retornará un error.";

            public const string UpdateUserHeader = "Actualiza un cliente existente [SQL Stored Procedure]";
            public const string UpdateUserDescription = "Este endpoint permite actualizar la información de un cliente específico utilizando su ID. Si el usuario no existe, se retornará un error. Los campos obligatorios deben ser proporcionados.";

            public const string DeleteUserHeader = "Elimina un cliente [SQL Stored Procedure]";
            public const string DeleteUserDescription = "Este endpoint permite eliminar un cliente específico utilizando su ID. Si el cliente no existe, se retornará un error.";

            public const string ReceiveUserDataHeader = "Recibe los datos del usuario";
            public const string ReceiveUserDataDescription = @" Este endpoint recibe una solicitud HTTP POST con el nombre y número de teléfono del usuario, "
                                                             + "los almacena y simula el envío de un mensaje de confirmación. Dichos registros quedan como mensaje "
                                                             + "a traves de los logs del sistema, Si alguno de los campos está vacío o nulo, se retornará un error.";
        }
    }
}

