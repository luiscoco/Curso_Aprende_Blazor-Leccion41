# CURSO: APRENDE BLAZOR

# LECCIÓN 41: Referencia entre Componentes

En esta lección vamos a crear dos Componentes. Un Componente Padre que contiene a un Componente Hijo. Creamos un referencia al Componente Hijo en el Componente Padre. Vamos a utilizar la referencia al Componente Hijo para llamar desde el Componente Padre a una función definida en el Componente Hijo.

1. Abrir la aplicación con Visual Studio 2022 o VSCode

2. Definimos una función de JavaScript en el archivo example.js

```javascript
window.showAlert = (message) => {
    alert(message);
}
```

3. Incluimos la localización del archivo example.js en el componente App.razor

```razor
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <base href="/" />
    <link rel="stylesheet" href="bootstrap/bootstrap.min.css" />
    <link rel="stylesheet" href="app.css" />
    <link rel="stylesheet" href="Leccion41.styles.css" />
    <link rel="icon" type="image/png" href="favicon.png" />
    <HeadOutlet @rendermode="InteractiveServer" />
</head>

<body>
    <Routes @rendermode="InteractiveServer" />
    <script src="_framework/blazor.web.js"></script>
    <script src="js/example.js"></script>
</body>

</html>
```

4. Creamos una clase en C# IJSRuntimeExtensions.cs para definir un método de extensión del interfaz IJSRuntime

```csharp
using Microsoft.JSInterop;

namespace Leccion40.Components.Helper
{
    public static class IJSRuntimeExtensions
    {
        public static async Task ShowAlert(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showAlert", message);
        }
    }
}
```

5. Creamos un Componente Padre ComponentePadre.razor

```razor
@page "/componentepadre"

<h3>ComponentePadre</h3>

<_ComponenteHijo @ref="childComponent"></_ComponenteHijo>

<div class="row">
    <button class="btn btn-success" @onclick="()=>childComponent.ShowAlert()" style="width: 250px;">Mostrar mensaje</button>
</div>

@code {
    private _ComponenteHijo? childComponent;
}
```

6. Creamos un Componente Hijo _ComponenteHijo.razor

```razor
@inject IJSRuntime JSRuntime
@using Leccion40.Components.Helper

<h3>ComponenteHijo</h3>

@code {

    public async Task ShowAlert()
    {
        await JSRuntime.ShowAlert("Hello from Blazor!");
    }
}
```

7. Incluimos el vínculo para navegar a nuestro Componente Padre

```razor
<div class="nav-item px-3">
    <NavLink class="nav-link" href="componentepadre">
        <span class="bi bi-list-nested-nav-menu" aria-hidden="true"></span> ComponentePadre
    </NavLink>
</div>
```

8. Ejecutamos la aplicación y verificamos el resultado obtenido
