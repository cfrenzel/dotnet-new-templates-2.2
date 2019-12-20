EntityFrameworkCore.MigrationsProject_2.2
=======

#### .NET Core 2.2 / EFCore 2.2 Template for keeping migrations scripts and DbContext in their own assembly

**Goals**

   * Keep DbContext and Migrations in their own assembly
   * Run the migrations as part of a deployment script

**To use set the Startup Project to the MigrationHelper Project**

  * Messaging.Samples.Persistence.EFCore.MigrationHelper

**To Add a Migration From the Package Manager Console**

  * Set DefaultProject to Messaging.Samples.Persistence.EFCore
  * Add-Migration Intitial

**To Update the Database  From the Package Manager Console**

  * Set DefaultProject to Messaging.Samples.Persistence.EFCore
	* Update-Database
