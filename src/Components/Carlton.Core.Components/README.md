<div align="center">
   <img src="../../../images/CarltonLogo.png" alt="Image Alt Text" width="200" />
</div>
</br>



# Carlton.Core.Components 

`Carlton.Core.Components` is a Blazor Component Library containing comprehensive collection of reusable components designed to simplify and accelerate the development of Blazor applications and create a uniform experience across the Project Carlton ecosystem.

![C#](https://img.shields.io/badge/language-C%23-blue)
![ASP.NET](https://img.shields.io/badge/ASP.NET-blue)
![Blazor](https://img.shields.io/badge/Blazor-blue)

## Key Features

* Accordions
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

#### CSS Variables from `vars.css`

Below are the CSS variables defined in the `vars.css` file along with their default values:

```css
:root [data-theme="light"] {
	/* Text */
	--primary-text-color: #5e5e5e;
	--secondary-text-color: #9a9a9a;
	--selected-color: #21c1d6;
	--font-family: 'Roboto', sans-serif;
	/* Accents */
	--accent-neutral: #808080;
	--accent-color-1: #1e88e5;
	--accent-color-2: #00897b;
	--accent-color-3: #e46a76;
	--accent-color-4: #ab8ce4;
	/* Layout Theming */
	--layout-main-background-color: #f4f3ef;
	--layout-nav-background-color: #212120;
	--layout-footer-background-color: #272b34;
	--layout-footer-color: #fff;
	--layout-header-background-color: #f4f3ef;
	--layout-header-icon-color: #5e5e5e;
	--layout-mobile-header-background-color: #212120;
	--layout-mobile-header-primary-text-color: #fff;
	--layout-mobile-header-icon-color: #fff;
	/* Component */
	--component-background-color: #fff;
	--component-secondary-background-color: #f2f2f2;
	--component-hover-background-color: #e9ecef;
	--component-selected-row-background-color: #c7e0f4;
}

:root [data-theme="dark"] {
	/* Text */
	--primary-text-color: #fff;
	--secondary-text-color: #a1aab2;
	--selected-color: #21c1d6;
	--font-family: 'Roboto', sans-serif;
	/* Accents */
	--accent-neutral: #808080;
	--accent-color-1: #1e88e5;
	--accent-color-2: #00897b;
	--accent-color-3: #e46a76;
	--accent-color-4: #ab8ce4;
	/* Layout Theming */
	--layout-main-background-color: #323743;
	--layout-nav-background-color: #212120;
	--layout-footer-background-color: #272b34;
	--layout-footer-color: #fff;
	--layout-header-background-color: #323743;
	--layout-header-icon-color: #fff;
	--layout-mobile-header-background-color: #212120;
	--layout-mobile-header-primary-text-color: #fff;
	--layout-mobile-header-icon-color: #fff;
	/* Component */
	--component-background-color: #272b34;
	--component-secondary-background-color: #333742;
	--component-hover-background-color: #828487;
	--component-selected-row-background-color: #21c1d6;
}

```

## Authors

Contributors names and contact info

Nicholas Galazzo  
nicholas.galazzo@gmail.com

Stephen Galazzo  
Stephen.Galazzo@gmail.com

