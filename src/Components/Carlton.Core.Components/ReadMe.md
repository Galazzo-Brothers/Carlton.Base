<h1 align="center">
    <img src=""/wwwroot/images/CarltonLogo.png" width="200" />
</br>
    Project Carlton
</br>

# Carlton.Core.Components 

A Blazor Component Library is a comprehensive collection of reusable components designed to simplify and accelerate the development of Blazor applications and create a uniform experience across the Project Carlton ecosystem.

![C#](https://img.shields.io/badge/language-C%23-blue)
![ASP.NET](https://img.shields.io/badge/ASP.NET-blue)
![Blazor](https://img.shields.io/badge/Blazor-blue)

## Key Features

* Buttons
* Dashboard Cards
* Checkboxes  
* Consoles
* Dropdowns
* Dynamic Component Wrappers
* Error Prompts
* Logo
* Footer
* Navigation
* Pills
* Spinner
* Tables
* Tabs
* Toasts

## Dependencies

* Carlton.Core.Foundation.Web

## Getting Started

### Installing

```bash
dotnet add package Carlton.Core.Components
```

### Customizing and Theming Instructions

1. **Locate the CSS Variables File**: Find the `vars.css` file within the root directory of the component library.

2. **Replace the CSS Variables File**:
   - Replace the original `vars.css` file with your own custom version containing updated CSS variables reflecting your desired styles.

2. **Verify Inclusion in `index.html`**:
   - Ensure that the updated `vars.css` file is included in the `index.html` file of your project. You can do this by adding a `<link>` tag to reference the CSS file within the `<head>` section of the HTML document.

```html
   <head>
       <!-- Other meta tags and external CSS files -->
       <link rel="stylesheet" href="path/to/your/vars.css">
   </head>
   ```

#### Sample CSS Variables from `vars.css`

Below are examples of CSS variables defined in the `vars.css` file along with their default values:

```css
/* Sample CSS Variables */
:root {
    --primary-color: #007bff;
    --secondary-color: #6c757d;
    --text-color: #212529;
    --background-color: #ffffff;
    /* Add more CSS variables here */
}
```

## Authors

Contributors names and contact info

Nicholas Galazzo  
nicholas.galazzo@gmail.com

Stephen Galazzo  
Stephen.Galazzo@gmail.com

