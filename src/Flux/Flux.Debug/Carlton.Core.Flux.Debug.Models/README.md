<div align="center">
    <img src="../../../images/CarltonLogo.png" alt="Carlton Logo" width="200" />
</div>
</br>

# Carlton.Core.Lab.Models

Welcome to the Carlton.Core.Lab.Models project! This project contains the models for the ViewModels and Commands used in the Carlton.Core.Lab framework.

## Overview

The Carlton.Core.Lab.Models project serves as a central repository for defining the data models used across the Carlton.Core.Lab framework. These models include ViewModels representing the state and behavior of components in the user interface, as well as Commands used to trigger actions and mutations within the framework.

## Purpose

The purpose of separating models into a dedicated project is to promote code reusability and maintainability. By defining models in a separate project, they can be easily shared between different parts of the application, including client-side Blazor components and server-side code. This approach ensures consistency and reduces duplication of code.

## Key Features

- **Centralized Model Definitions**: All models used within the Carlton.Core.Lab.Models project are defined in a single repository, making it easy to manage and maintain the codebase.

- **Shared Between Client and Server**: Models defined in this project can be shared between client-side Blazor components and server-side code, enabling seamless communication and data exchange across different parts of the application.

- **Strongly Typed**: Models are strongly typed using C# classes, providing compile-time safety and preventing runtime errors related to data type mismatches.

- **Encapsulated Validation**: Models utilize DataAnnotation validation, allowing them to encapsulate their own superficial validation rules. This approach promotes code reuse and simplifies validation logic across components.

## Usage

To use the models defined in the Carlton.Core.Lab.Models project, simply reference the project in your solution and import the necessary namespaces into your code files. You can then instantiate and manipulate the models as needed within your application.

## Dependencies

The Carlton.Core.Lab.Models project does not have any external dependencies and consists solely of C# poco objects, making it easy to integrate into your application.

## Authors

- Nicholas Galazzo  
  Email: nicholas.galazzo@gmail.com

- Stephen Galazzo  
  Email: Stephen.Galazzo@gmail.com
