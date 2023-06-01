//Device name: Obtained using Environment.MachineName.
//Activation status: Retrieved using WMI (Windows Management Instrumentation) by querying the SoftwareLicensingService class.
//Storage information: The total and free storage space on the C drive is obtained using the DriveInfo class.
//RAM capacity: The total physical memory is retrieved using the Win32_ComputerSystem class.
//Processor information: The processor name is obtained by querying the Win32_Processor class.
//Note: Make sure to add a reference to the System.Management assembly in your project for this code to work properly
