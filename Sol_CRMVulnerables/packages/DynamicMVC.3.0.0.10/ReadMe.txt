Getting Started
If you are new to Dynamic MVC, follow the instructions in the DynamicMVC_ReadMe file.

Release Notes 3.0
-Dynamic MVC has been restructured to use unity.  This will allow developers to inject any code at any point to override Dynamic MVC native code.
-Dynamic MVC has been restructured into several projects.
-DynamicMVC.Annoations allows you to reference all attributes that control Dynamic MVC from a seperate project.  This is similar to how DataAnnotations work in .net.
-Many files were added to the DynamicMVC folder.  This will make it easier for developers to modify the code.  Any code contributions are welcome.
-DynamicMethod, DynamicOperation, and DynamicMethodInvoker's have been added to allow more rebust business logic to be added easily.
-Message have been added to every dynamic view.  If viewdata or temp data contains a Message key with MessageViewModel, that will be displayed on the page.

Release Notes 2.0
-Dynamic MVC 2.0 has a new mobile responsive index view.
-DynamicEntity attribute has a new EntityType property that allows you to seperate your metadata from your entities.  This is useful if your model objects are in a seperate project or dll.
-Inheriting from DynamicController is no longer supported.  Attributes aiding in inheritence have been removed.  The best way to customize the controller for a specific entity is to simply copy it and rename the copy using mvc naming conventions.