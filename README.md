# CaseMaker  

## *Easy creation of radiology teaching file cases from PACS*
 
**CaseMaker is a Windows program to facilitate the creation of radiology cases for both local storage as well as submission to an RSNA MIRC server.  Key features:**

* Accepts **drag-and-drop** of .bmp, .jpg, .gif, .png, and .tif images from other Windows applications (with special support for **Fujifilm Synapse PACS**) or from Windows Explorer.
* **Automatically populates** patient data from DICOM headers when images are dragged from Fujifilm Synapse PACS.
* Allows drag-and-drop of images from CaseMaker to other applications or to Windows Explorer.
* Cases in Synapse PACS can be launched by double-clicking images in CaseMaker.
* Allows drag-and-drop rearrangement of the order of images in a case.
* Cases saved as .zip files, with metadata stored as readable XML according to the [RSNA MIRCdocument Schema](http://mircwiki.rsna.org/index.php?title=The_MIRCdocument_Schema).
* One-click preview of XSL-transformed MIRC document in teaching file format.
* **Small and portable** single executable, runs from a USB key without installation.

This project uses the [DotNetZip Library](http://dotnetzip.codeplex.com).

Requires .NET Framework 2.0 or higher.
