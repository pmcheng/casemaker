# CaseMaker  

## *Easy creation of radiology teaching file cases from PACS*
 
**CaseMaker is a Windows program to facilitate the creation of radiology cases for both local storage as well as submission to an RSNA MIRC server.  Key features:**

* Accepts **drag-and-drop** of .bmp, .jpg, .gif, .png, and .tif images from other Windows applications (with special support for **Fujifilm Synapse PACS**) or from Windows Explorer.
* **Automatically populates** patient data from DICOM headers when images are dragged from Fujifilm Synapse PACS.
  * In Synapse 7, images can no longer be dragged from the viewport; a workaround is to use Ctrl-C on desired image before dragging.
* Allows drag-and-drop of images from CaseMaker to other applications or to Windows Explorer.
* Cases in Synapse PACS can be launched by double-clicking images in CaseMaker.
* Allows drag-and-drop rearrangement of the order of images in a case.
* Cases saved as .zip files, with metadata stored as readable XML according to the [RSNA MIRCdocument Schema](http://mircwiki.rsna.org/index.php?title=The_MIRCdocument_Schema).
* One-click preview of XSL-transformed MIRC document in teaching file format.
* **Small and portable** single executable, runs from a USB key without installation.

This project uses the [DotNetZip Library](http://dotnetzip.codeplex.com).

Requires .NET Framework 4.7.2 or higher.


----------

CaseMaker is a stand-alone .exe file.  However, an optional configuration XML file (casemaker_config.xml) can be placed in the same directory as the executable file.  This file provides location strings to assign to drag-and-drop items that have matching elements in the URL, as well as upload URL's for the RSNA Teaching File Service.  An example configuration file is as follows:

	<?xml version="1.0" encoding="utf-8"?>
	<config>
	<dictLocation>
  		<location matchstring="datasource=https%253A%252F%252Fexternal.synapse.uscuh.com" site="Keck/Norris" />
  		<location matchstring="datasource=http%253A%252F%252Fsynapse.uscuh.com" site="Keck/Norris" />
  		<location matchstring="datasource=https%253A%252F%252Ffujipacs.hsc.usc.edu" site="HCC2" />
  		<location matchstring="datasource=http%253A%252F%252Fhcc2synvweb" site="HCC2" />
  		<location matchstring="datasource=http%253A%252F%252Flacsynapse" site="LACUSC" />
	</dictLocation>
		<listUpload>
  			<site url="http://10.131.12.41:8080/submit/ss1" />
  			<site url="http://192.168.0.101:8080/submit/ss1" />
		</listUpload>
	</config>

If the drag-and-drop URL contains *matchstring*, the site string is used for the location.  If there are no matching *matchstring*s, the DICOM header is used for the location.  This is useful when the DICOM location is not uniform for all the devices at one site, but we would like to tag them all with the same standardized location string.

The URLs in the upload list are the URLs for the Zip service of the RSNA Teaching File System, used in the dropdown box in the upload dialog.
