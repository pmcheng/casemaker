<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">

<xsl:param name="input-type">button</xsl:param>
<xsl:param name="unknown">no</xsl:param>
<xsl:param name="context-path"/>
<xsl:param name="dir-path"/>
<xsl:param name="doc-path"/>
<xsl:param name="preview">no</xsl:param>
<xsl:param name="export-url"/>
<xsl:param name="edit-url"/>
<xsl:param name="add-url"/>
<xsl:param name="publish-url"/>
<xsl:param name="delete-url"/>
<xsl:param name="user-is-owner"/>
<xsl:param name="user-is-admin"/>
<xsl:param name="filecabinet-url"/>
<xsl:param name="doc-url"/>
<xsl:param name="server-url"/>
<xsl:param name="dicom-export-url"/>
<xsl:param name="database-export-url"/>
<xsl:param name="bgcolor"/>
<xsl:param name="display"/>
<xsl:param name="icons"/>
<xsl:param name="today"/>

<xsl:template match="*|@*|text()">
	<xsl:copy>
		<xsl:apply-templates select="*|@*|text()" />
	</xsl:copy>
</xsl:template>

<xsl:template match="/MIRCdocument">
	<html>
		<head>
      <style>
        <![CDATA[
      .show {visibility:visible; display:block;}
.hide {visibility:hidden; display:none;}

li {text-align:left; margin-left:5%;}

/********* tab button styles *********/
.u {
	color:white;
	background-color:dodgerblue;
	font-weight:bold;
	border:0;
	margin:2px;
	margin-bottom:0;
	}
.s {color:white;
	background-color:blue;
	font-weight:bold;
	border:0;
	margin:2px;
	margin-bottom:0;
	}

/******* document footer styles *******/
table.footer {
	margin:auto;
	border:thin solid gray;
	}
table.footer th {
	font-weight:bold;
	padding:3;
	}
table.footer td {
	text-align:center;
	padding-left:10;
	padding-right:10;
	}
table.footer td input {
	width:100%;
	padding-left:3;
	padding-right:3;
	}

/************ page styles ************/
body.page {
	font-size:13pt;
	font-family:arial;
	}
body.page h1 {
	font-size:20pt;
	text-align:center;
	}
body.page p {
	margin-left:5%;
	}
body.page p.center {
	text-align:center;
	margin-left:0; 
	}
body.page p.authorname {
	margin-left:0; 
	text-align:center;
	font-weight:bold;
	}
body.page p.authorinfo {
	text-align:center;
	margin-left:0; 
	margin-top:0; 
	margin-bottom:0;
	}
body.page hr.docfooter {
	text-align:center;
	margin-top:20;
	width:60%; 
	}
body.page p.centeredcaption {
	text-align:center;
	margin:0; 
	}

/************ tab styles ************/
body.tab {
	font-size:13pt;
	font-family:arial;
	margin:0; 
	border:0; 
	padding:8; 
	padding-bottom:0; 
	overflow:hidden;
	}
body.tab div.maindiv {
	border:1px #aaccdd solid; 
	overflow:auto;
	}
body.tab h1 {
	font-size:20pt;
	text-align:center;
	}
body.tab p {
	margin-left:5%;
	}
body.tab p.center {
	margin-left:0; 
	text-align:center;
	}
body.tab p.authorname {
	margin-left:0; 
	text-align:center;
	font-weight:bold;
	}
body.tab p.authorinfo {
	margin-left:0; 
	text-align:center;
	margin-top:0; 
	margin-bottom:0;
	}
body.tab hr.docfooter {
	text-align:center;
	margin-top:20;
	width:60%; 
	}

/************ mstf styles ************/
body.mstf {
	font-size:13pt;
	font-family:arial;
	margin:0; 
	border:0; 
	padding:8; 
	padding-bottom:0; 
	overflow:hidden;
	}
body.mstf div.maindiv {
	width:100%; 
	border:0; 
	padding:0; 
	margin:0; 
	overflow:hidden;
	}
body.mstf h1 {
	font-size:12pt;
	font-family:sans-serif; 
	margin:5; 
	margin-left:0;
	}
body.mstf p {
	margin:5;
	margin-top:10;
	margin-left:0;
	}
body.mstf p.center {
	text-align:center;
	margin-left:0; 
	}
body.mstf p.authorname {
	margin-left:0;
	}
body.mstf p.authorinfo {
	margin-left:0; 
	margin-top:0; 
	margin-bottom:0
	}
body.mstf hr.docfooter {
	text-align:center;
	margin-top:10;
	width:60%;
	}
body.mstf ul {
	margin-left:20;
	}
body.mstf li {
	margin-left:0;
	}

/*********** patient table styles*************/
body.page table.mdtable {
	margin-left:20;
	margin-right:20;
	border-collapse:collapse;
	border:thin solid #88bbff;
}

body.tab table.mdtable {
	margin-left:20;
	margin-right:20;
	border-collapse:collapse;
	border:thin solid #88bbff;
}

body.mstf table.mdtable {
	margin-left:0;
	margin-right:5;
	border-collapse:collapse;
	border:thin solid #88bbff;
}

table.pttable {
	width:60%;
	margin:auto;
	border-collapse:collapse;
	border:thin solid #88bbff;
}

table.pttable td {
	padding:3;
}

table.pttable td.ptlabel {
	width:10%;
}

/*********** RadLex term styles*************/
a.term {
	text-decoration:none;
}
a.term:hover {
	text-decoration: underline;
}

/*********** metadata table styles*************/
table.mdtable td {
	border:thin solid #88bbff;
	padding:5;
	padding-left:10;
	padding-right:10;
}

/*********** image-section styles*************/
.imagediv {
	width:100%;
	margin:0;
	border:0;
	padding:0;
	padding-bottom:0;
	overflow:hidden;
	}
.leftside {
	width:256px; 
	border:0; 
	margin:0; 
	padding:0; 
	overflow:hidden;
	}
.tokenbutton {
	border:0;
	margin:1;
	padding:0;
	width:64;
	}
.left {
	text-align:left;
	border:1px #aaccdd solid;
	overflow:auto;
	margin:0;
	padding:0;
	}
.rightside {
	border:1px #aaccdd solid; 
	background:#111111; 
	position:absolute; 
	margin:0; 
	padding:0; 
	overflow:hidden;
	}
.rbuttons {
	border:0;
	margin:0;
	background:#111111;
	padding:2;
	text-align:left;
	padding:2;
	margin:2;
	height:22px;
	}
.imagenumber {
	font-weight:bold;
	font-family:sans-serif;
	font-size:small;
	color:white;
	}
.imagenav {
	float:left;
	font-weight:bold;
	font-family:sans-serif;
	font-size:small;
	color:white;
	}
.selbuttons {
	float:right;
	text-align:right;
	font-weight:bold;
	font-family:sans-serif;
	font-size:small;
	color:white;
	}
.rimage {
	border:0;
	margin:0;
	padding:0;
	overflow:auto;
	}
.captions {
	display:block;
	text-align:center;
	font-weight:bold;
	font-family:sans-serif;
	font-size:small;
	color:white;
	margin:0;
	padding:0;
	}

      ]]></style>
			<xsl:call-template name="script-init"/>
			<script>
        <![CDATA[
//This section provides multi-browser support for certain
//commonly required functions.
var IE = document.all;

//Get the top of an element in its parent's coordinates
function getOffsetTop(theElement) {
	return theElement.offsetTop;
}

//Get the source of the event.
function getSource(theEvent) {
	return (IE) ?
		theEvent.srcElement : theEvent.target;
}

function getEvent(theEvent) {
	return (theEvent) ? theEvent : ((window.event) ? window.event : null);
}

function openURL(url,target) {
	window.open(url,target);
}

//Get the displayed width of an object
function getObjectWidth(obj) {
	return obj.offsetWidth;
}

//Get the displayed height of an object
function getObjectHeight(obj) {
	return obj.offsetHeight;
}      
      ]]>
			</script>
			<script>
        <![CDATA[
//This section provides multi-browser support for certain
//commonly required functions.

//Get the height of the window.
function getHeight() {
	return (IE) ?
		document.body.clientHeight : window.innerHeight - 10;
}

//Get the width of the window.
function getWidth() {
	return (IE) ?
		document.body.clientWidth : window.innerWidth;
}

//Get the displayed width of an object
function getObjectWidth(obj) {
	return obj.offsetWidth;
}

//Get the displayed height of an object
function getObjectHeight(obj) {
	return obj.offsetHeight;
}

//Open a url
function openURL(url,target) {
	window.open(url,target);
}
//end of the multi-browser functions

function copy(event) {
	if (IE) {
		var source = getSource(getEvent(event));
		var textarea = document.createElement("TEXTAREA");
		textarea.innerText = source.innerText;
		var copy = textarea.createTextRange();
		copy.execCommand("Copy");
	}
}

function copyTextToClipboard(text) {
	if (IE) {
		var textarea = document.createElement("TEXTAREA");
		textarea.innerText = text;
		var copy = textarea.createTextRange();
		copy.execCommand("Copy");
	}
}

window.onresize = setSize;
window.onload = mirc_onload;
document.onkeydown = keyDown;
document.onkeyup = keyUp;

function mirc_onload() {

	setBackground();
	if ((display == "mstf") || (display == "mirctf") || (display == "tab")) {
		initTabs();
		setSize();
		if (imageSection == "yes") loadImage(1);
		setIFrameSrc(currentPage);
	}
	setWheelDriver();
	window.focus();
}

function setBackground() {
	if (background == "light") {
		document.body.style.color = "black";
		document.body.style.background = "white";
	}
	else {
		document.body.style.color = "white";
		document.body.style.background = "black";
		document.body.setAttribute("link","white");
	}
}

//save images button handler
function saveImages(fileurl) {
	openURL(fileurl,"_self");
}

//export button zip extension handler
function exportZipFile(url,target, myEvent) {
	if (getEvent(myEvent).altKey) url += "&ext=mrz";
	openURL(url,target);
}

//document deletion function
function deleteDocument(url) {
	if (confirm("Are you sure you want\nto delete this document\nfrom the server?")) {
		openURL(url,"_self");
	}
}

//quiz answer event handler (toggle response)
function toggleResponse(divid) {
	var x = document.getElementById(divid);
	if (x.style.display == 'block') {
		x.style.display = 'none';
		x.style.visibility = 'hidden';
		if (!document.all) window.resizeBy(0,1);
	} else {
		x.style.display = 'block';
		x.style.visibility = 'visible';
		if (!document.all) window.resizeBy(0,-1);
	}
}

//jump button event handler for text-captions
function jumpButton(inc, myEvent) {
	var source = getSource(getEvent(myEvent));
	var buttons = source.parentElement.getElementsByTagName("input");
	var i;
	for (i=0; i<buttons.length; i++) {
		if (buttons[i] === source) {
			var b = i + inc;
			if ((b >= 0) && (b < buttons.length)) buttons[b].scrollIntoView(false);
			return;
		}
	}
}

//show button event handler for text-captions
function showButton(myEvent) {
	var source = getSource(getEvent(myEvent));
	var captionDiv = getPreviousNamedSibling(source,"div");
	if (captionDiv.className == "hide") {
		captionDiv.className="show";
    	if (inputType == "image")
			source.src = contextPath + "/buttons/hidecaption.png";
		else if (inputType == "button")
			source.value = "Hide Caption";
	} else {
		captionDiv.className="hide";
    	if (inputType == "image")
			source.src = contextPath + "/buttons/showcaption.png";
		else if (inputType == "button")
			source.value = "Show Caption";
	}
	source.scrollIntoView(false);
}

function getPreviousNamedSibling(start,name) {
	var prev = start.previousSibling;
	while ((prev != null) && (prev.tagName.toLowerCase() != name))
		prev = prev.previousSibling;
	return prev;
}

//initialize the tabs
function initTabs() {
	currentPage = document.getElementById('tab'+firstTab);
	currentButton = document.getElementById('b'+firstTab);
	if (currentPage == null) {
		currentPage = document.getElementById('tab1');
		currentButton = document.getElementById('b1');
	}
	currentPage.className = "show";
	currentButton.className = "s";
	if (showEmptyTabs != "yes") {
		var n = 2;
		var done = false;
		while (!done) {
			var b = document.getElementById("b"+n);
			if (b != null) {
				if (n != firstTab) {
					var d = document.getElementById("tab"+n);
					var count = 0;
					for (var q=0; q<d.childNodes.length; q++) {
						if (d.childNodes[q].nodeType == 1) count++;
					}
					if (count < 2) {
						b.style.display = 'none';
						b.style.visibility = 'hidden';
					}
				}
				n += 1;
			}
			else done = true;
		}
	}
}

//tab button event handler
var currentPage;
var currentButton;
function bclick(b,np) {
	currentButton.className = "u";
	currentButton = document.getElementById(b);
	currentButton.className = "s";
	var nextPage = document.getElementById(np);
	if (currentPage != nextPage) {
		currentPage.className="hide";
		currentPage = nextPage;
		currentPage.className="show";
		setIFrameSrc(currentPage);
		var rightside = document.getElementById("rightside");
		if (rightside != null) {
			var imagetab = rightside.parentNode.id;
			var maindiv = document.getElementById("maindiv");
			if (np == imagetab)
				maindiv.style.overflow = "hidden";
			else
				maindiv.style.overflow = "auto";
		}
	}
}

//set the src attribute of all the iframe children in an element.
//(this is just to avoid the error message from IE when an
//iframe is loaded in a non-visible tab and the iframe tries
//to grab the focus.)
function setIFrameSrc(el) {
	var iframes = el.getElementsByTagName("IFRAME");
	for (var i=0; i<iframes.length; i++) {
		var fr = iframes[i];
		var src = fr.getAttribute("src");
		var longdesc = fr.getAttribute("longdesc");
		if ((longdesc) && (longdesc != "")) {
			fr.src = longdesc;
			fr.setAttribute("longdesc", "");
		}
	}
}

//set the sizes for the components of the document
var rightsideWidth = 700;
var leftsideWidth = 256;

function setSize() {
	if ((display == "mstf") || (display == "mirctf") || (display == "tab")) {
		var maindiv = document.getElementById('maindiv');
		var maindivHeight = getHeight() - maindiv.offsetTop;
		if (maindivHeight < 100) maindivHeight = 100;
		maindiv.style.height = maindivHeight;

		if (imageSection == "yes") {

			var leftside = document.getElementById('leftside');
			var uldiv = document.getElementById('uldiv');
			var lldiv = document.getElementById('lldiv');
			var rightside = document.getElementById('rightside');
			var rbuttons = document.getElementById('rbuttons');
			var captions = document.getElementById('captions');
			var rimage = document.getElementById('rimage');

			var absoluteMinimum = 100;
			var rightsideMinimumWidth = 512;
			var leftsideMinimumWidth = 256;
			if (display == "tab") leftsideMinimumWidth = 128;

			rightsideWidth = imagePaneWidth;
			if ((uldiv == null) && (lldiv == null)) {
				rightsideWidth = getWidth();
				leftsideWidth = 0;
			} else {
				var widthMargin = 18;
				var width = getWidth() - widthMargin;
				var rightMargin = 17;
				rightsideWidth += rightMargin;
				if (rightsideWidth < rightsideMinimumWidth)
					rightsideWidth = rightsideMinimumWidth;
				leftsideWidth = width - rightsideWidth;
				if (leftsideWidth < leftsideMinimumWidth)
					leftsideWidth = leftsideMinimumWidth;
				rightsideWidth = width - leftsideWidth;
				if (rightsideWidth < absoluteMinimum)
					rightsideWidth = absoluteMinimum;
			}
			var rightsideTop = 0;
			var rightsideLeft = leftsideWidth;
			if (!IE) {
				rightsideTop = maindiv.offsetTop;
				rightsideLeft = leftsideWidth + 8;
			}

			leftside.style.width = leftsideWidth;
			leftside.style.height = maindivHeight - 2;

			rightside.style.width = rightsideWidth;
			rightside.style.top = rightsideTop;
			rightside.style.left = rightsideLeft;
			rightside.style.height = maindivHeight - 2;

			if ((uldiv != null) && (lldiv != null)) {
				var uldivHeight = maindivHeight * 2/3;
				uldiv.style.height = uldivHeight;
				lldiv.style.height = maindivHeight - uldivHeight - 1;
			}
			else if ((uldiv == null) && (lldiv != null)) {
				lldiv.style.height = maindivHeight - 1;
			}
			else if ((uldiv != null) && (lldiv == null)) {
				uldiv.style.height = maindivHeight - 1;
			}

			rimage.style.width = rightsideWidth;
			rimage.style.height = maindivHeight - rbuttons.offsetHeight - 1;
		}
	}
}

//display images in image-sections
var currentImage = 0;
var annotationDisplayed = false;
var annotationIsSVG = false;
var pImage = 1;		//primary displayable image (base)
var sImage = 2;		//annotated svg XML file
var aImage = 3;		//annotated displayable image
var osImage = 4;	//original size displayable image
var ofImage = 5;	//original format image (e.g. DICOM)
var aCaption = 6;	//display='always' caption
var cCaption = 7;	//display='click' caption
var cFlag = 8;		//clickable has been shown (0=no; 1=yes)

function loadImage(image) {
	displayImage(image);
}

function loadAnnotation(image) {
	if (displayImage(image)) displayAnnotation();
}

//This code is for loading annotations in response to the ctrl key.
var ctrlKeyCode = 17;
function keyDown(theEvent) {
	theEvent = getEvent(theEvent);
	if (theEvent.keyCode == ctrlKeyCode) {
		if (!annotationDisplayed) displayAnnotation();
	}
}
function keyUp(theEvent) {
	theEvent = getEvent(theEvent);
	if (theEvent.keyCode == ctrlKeyCode) {
		if (annotationDisplayed) displayImage(currentImage);
	}
}

function displayImage(image) {
	if ((image > 0) && (image <= images.length)) {
		var place = document.getElementById('rimagecenter');
		var src = images[image-1][pImage];
		while (place.firstChild != null) place.removeChild(place.firstChild);
		place.appendChild(makeElement(src));
		var imagenumber = document.getElementById('imagenumber');
		while (imagenumber.firstChild != null) imagenumber.removeChild(imagenumber.firstChild);
		imagenumber.appendChild(document.createTextNode("Image: " + image));

		setCaptions(image);
		setSize();

		var b;
		if ((images[image-1][aImage] != "") || (images[image-1][sImage] != ""))
			enableButton('annbtn','inline');
		else
			disableButton('annbtn','none');

		if (images[image-1][osImage] != "")
			enableButton('orgbtn','inline');
		else
			disableButton('orgbtn','none');

		if (images[image-1][ofImage] != "")
			enableButton('dcmbtn','inline')
		else
			disableButton('dcmbtn','none');

		if (image > 1)
			enableButton('previmg','inline');
		else
			disableButton('previmg','inline');

		if (image < images.length)
			enableButton('nextimg','inline');
		else
			disableButton('nextimg','inline');

		annotationDisplayed = false;
		currentImage = image;
		return true;
	}
	return false;
}

function setCaptions(image) {
	var capdiv = document.getElementById('captions');
	while (capdiv.firstChild != null) capdiv.removeChild(capdiv.firstChild);
	if (images[image-1][aCaption] != '') {
		setCaption(capdiv,images[image-1][aCaption],false);
	}
	if (images[image-1][cCaption] != '') {
		if (images[image-1][cFlag] != 0) setCaption(capdiv,images[image-1][cCaption],false);
	 	else setCaption(capdiv,'more...',true);
	}
}

function setCaption(parent,caption,clickable) {
	var p = document.createElement("P");
	p.style.marginTop = 0;
	var t = document.createTextNode(caption);
	if (clickable) {
		p.onclick = showClickableCaption;
		var u = document.createElement("U");
		u.appendChild(t);
		p.appendChild(u);
	}
	else p.appendChild(t);
	parent.appendChild(p);
}

function showClickableCaption() {
	if ((currentImage > 0) && (images[currentImage-1][cCaption] != '')) {
		var capdiv = document.getElementById('captions');
		var pList = capdiv.getElementsByTagName("P");
		capdiv.removeChild(pList[pList.length-1]);
		setCaption(capdiv,images[currentImage-1][cCaption],false);
		images[currentImage-1][cFlag] = 1;
	}
}

function disableButton(id,display) {
	var b = document.getElementById(id)
	b.disabled = true;
	b.style.backgroundColor = 'gray';
	b.style.fontWeight = 'bold';
	b.style.visibility = 'hidden';
	b.style.display = display;
}

function enableButton(id,display) {
	var b = document.getElementById(id)
	b.disabled = false;
	b.style.backgroundColor = 'dodgerblue';
	b.style.color = 'white';
	b.style.fontWeight = 'bold';
	b.style.visibility = 'visible';
	b.style.display = display;
}

function displayAnnotation() {
	if (currentImage > 0) {
		if (annotationDisplayed) loadImage(currentImage);
		else {
			//Check the aImage and sImage values to determine
			//whether this is an SVG annotation or an image
			//with burned-in annotations. Give precedence to
			//the burned-in image because it loads faster and
			//doesn't require the SVG viewer.
			var place = document.getElementById('rimagecenter');
			if (images[currentImage-1][aImage] != "") {
				//Image
				while (place.firstChild != null) place.removeChild(place.firstChild);
				place.appendChild(makeElement(images[currentImage-1][aImage]));
				annotationIsSVG = false;
				annotationDisplayed = true;
			}
			else if (images[currentImage-1][sImage] != "") {
				//SVG
				//Get the size of the image currently displayed.
				var img = place.getElementsByTagName("IMG")[0];
				if (img == null) return;
				var width = getObjectWidth(img);
				var height = getObjectHeight(img);

				//Embed the SVG file
				var svgEmbedID = "svgEmbedID";
				var embed = document.createElement("EMBED");
				embed.setAttribute("id",svgEmbedID);
				embed.setAttribute("src",images[currentImage-1][sImage]);
				embed.setAttribute("width",width);
				embed.setAttribute("height",height);
				embed.setAttribute("type","image/svg+xml");
				while (place.firstChild != null) place.removeChild(place.firstChild);
				place.appendChild(embed);

				//Set up the root element.
				//Get the href for the base image.
				var href = images[currentImage-1][pImage];
				configureSVGRoot(svgEmbedID,href);
				annotationIsSVG = true;
				annotationDisplayed = true;
			}
		}
	}
}

function configureSVGRoot(id,href) {
	try {
		var embed = document.getElementById(id);
		var width = embed.getAttribute("width");
		var height = embed.getAttribute("height");
		var svgdoc = embed.getSVGDocument();

		var root = svgdoc.rootElement;
		root.setAttribute("width",width);
		root.setAttribute("height",height);
		var viewBox = "0 0 " + width + " " + height;
		root.setAttribute("viewBox",viewBox);

		var imageElements = root.getElementsByTagName("image");
		if (imageElements.length == 0) {
			var image = svgdoc.createElementNS("http://www.w3.org/2000/svg", "image");
			image.setAttributeNS("http://www.w3.org/1999/xlink","href",href);
			image.setAttribute("x",0);
			image.setAttribute("y",0);
			image.setAttribute("width",width);
			image.setAttribute("height",height);
			if (root.firstChild == null) root.appendChild(image);
			else root.insertBefore(image,root.firstChild);
		}
	}
	catch (ex) {
		setTimeout("configureSVGRoot(\""+id+"\",\""+href+"\");",10);
	}
}

function makeElement(src) {
	var elem = document.createElement("IMG");
	elem.setAttribute("src",src);
	return elem;
}

function fetchOriginal() {
	if (currentImage > 0) {
		window.open(images[currentImage-1][osImage],"_blank");
	}
}

function fetchModality(myEvent) {
	if (currentImage > 0) {
		var imagePath = contextPath + dirPath + images[currentImage-1][ofImage];
		if (getEvent(myEvent).altKey)
			//alt key generates a DICOM dataset dump
			openURL(imagePath+"?dicom","_blank");
		else if (getEvent(myEvent).ctrlKey)
			//ctrl key downloads the file
			openURL(imagePath,"_self");
		else {
			//request the DICOM viewer applet if there are no modifiers
			var iframe = document.createElement("IFRAME");
			iframe.setAttribute("src",imagePath+"?viewer");
			iframe.setAttribute("width","600");
			iframe.setAttribute("height","600");
			//now load the iframe
			var place = document.getElementById('rimagecenter');
			while (place.firstChild != null) place.removeChild(place.firstChild);
			place.appendChild(iframe);
		}
	}
}

function addParam(applet,name,value) {
	var param = document.createElement("PARAM");
	param.setAttribute(name,value);
	applet.appendChild(param);
}

//handle a link to a metadata file.
function fetchFile(url, myEvent) {
	if (!getEvent(myEvent).altKey) openURL(url,"_blank");
	else openURL(url+"?dicom","_blank");
}

//set a case of the day
var xmlHttp;
function setCaseOfTheDay() {
	var url = document.getElementById("codurl").value;
	var title = document.getElementById("codtitle").value;
	var desc = document.getElementById("coddesc").value;
	var link = document.getElementById("codlink").value;
	var image = document.getElementById("codimage").value;
	var qs = "title=" + escape(title)
			+ "&description=" + escape(desc)
			+ "&link=" + escape(link)
			+ "&image=" + escape(image)
			+ "&timestamp=" + new Date().getTime();
	xmlHttp = getXmlHttp();
	xmlHttp.open("POST", url, true);
	xmlHttp.onreadystatechange = handleStateChange;
	xmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	xmlHttp.send(qs);
}

function handleStateChange() {
	if (xmlHttp.readyState == 4) {
		if (xmlHttp.status == 200) {
			alert("The document was entered as the Case of the Day.");
		}
	}
}

function getXmlHttp() {
	if (window.ActiveXObject) return new ActiveXObject("Microsoft.XMLHTTP");
	else if (window.XMLHttpRequest) return new XMLHttpRequest();
}


function setWheelDriver() {
	if ((display == "mstf") || (display == "mirctf") || (display == "tab")) {
		var rimage = document.getElementById("rimage");
		if (rimage) {
			if (rimage.addEventListener) {
				//Mozilla
				rimage.addEventListener('DOMMouseScroll', wheel, false);
			}
			else {
				// IE/Opera
				rimage.onmousewheel = wheel;
			}
			getImages();
		}
	}
}

function wheel(event){
	var delta = 0;
	if (!event) event = window.event;
	if (event.wheelDelta) {
		// IE/Opera.
	    delta = event.wheelDelta/120;
		if (window.opera) delta = -delta;
	}
	else if (event.detail) {
		// Mozilla
		delta = -event.detail/3;
	}
	if (delta < 0) delta = -1;
	if (delta > 0) delta = +1;
	if (!event.ctrlKey) loadImage(currentImage - delta);
	else loadAnnotation(currentImage - delta);
	if (event.preventDefault) event.preventDefault();
	event.returnValue = false;
}

var loadReq;
function getImages() {
	loadReq = getXmlHttp();
	getImage(0);
}

var nextToLoad;
function getImage(i) {
	if (i < images.length) {
		var src = images[i][pImage];
		nextToLoad = i + 1;
		loadReq.open("GET", src, true);
		loadReq.onreadystatechange = getNextImage;
		loadReq.send(null);
	}
}

function getNextImage() {
	if ((loadReq.readyState == 4) && (loadReq.status == 200)) {
		window.setTimeout("getImage(nextToLoad);",10);
	}
}
      
      ]]>
			</script>
			<title>MIRC document</title>
		</head>
		<xsl:choose>
			<xsl:when test="$display='tab'">
				<xsl:call-template name="tab-body"/>
			</xsl:when>
			<xsl:when test="$display='mstf' or $display='mirctf'">
				<xsl:call-template name="mstf-body"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="page-body"/>
			</xsl:otherwise>
		</xsl:choose>
	</html>
</xsl:template>

<xsl:template name="page-body">
	<body class="page">
		<xsl:call-template name="page-title"/>
		<xsl:apply-templates select="author"/>
		<xsl:call-template name="page-abstract"/>
		<xsl:if test="not($unknown='yes')">
			<xsl:apply-templates select="keywords"/>
		</xsl:if>
		<xsl:apply-templates select="section[
			((@visible='owner') and ($user-is-owner='yes')) or
			(
			 not(@visible='no') and not((@visible='owner') and
			 not($user-is-owner='yes')) and
			 (not(@after) or (number($today) &gt; number(@after)))
			)] | image-section"/>
		<xsl:apply-templates select="references"/>
		<xsl:call-template name="document-footer"/>
	</body>
</xsl:template>

<xsl:template name="tab-body">
	<xsl:variable name="sectionlist" select="section[
			((@visible='owner') and ($user-is-owner='yes')) or
			(
			 not(@visible='no') and
			 not((@visible='owner') and not($user-is-owner='yes')) and
			 (not(@after) or (number($today) &gt; number(@after)))
			)] | image-section"/>
	<xsl:variable name="referenceslist" select="references"/>
	<body class="tab">
		<xsl:call-template name="make-tabs">
			<xsl:with-param name="sectionlist" select="$sectionlist"/>
			<xsl:with-param name="referenceslist" select="$referenceslist"/>
		</xsl:call-template>
		<div id="maindiv" class="maindiv">
			<xsl:call-template name="make-section-divs">
				<xsl:with-param name="sectionlist" select="$sectionlist"/>
				<xsl:with-param name="referenceslist" select="$referenceslist"/>
			</xsl:call-template>
		</div>
	</body>
</xsl:template>

<xsl:template name="mstf-body">
	<xsl:variable name="sectionlist" select="section[
			((@visible='owner') and ($user-is-owner='yes')) or
			(
			 not(@visible='no') and
			 not((@visible='owner') and not($user-is-owner='yes')) and
			 (not(@after) or (number($today) &gt; number(@after)))
			)]"/>
	<xsl:variable name="referenceslist" select="references"/>
	<body class="mstf">
		<xsl:call-template name="make-tabs">
			<xsl:with-param name="sectionlist" select="$sectionlist"/>
			<xsl:with-param name="referenceslist" select="$referenceslist"/>
		</xsl:call-template>
		<div id="maindiv" class="maindiv">
			<div id="leftside" class="leftside">
				<div id="uldiv" class="left">
					<xsl:call-template name="make-section-divs">
					<xsl:with-param name="sectionlist" select="$sectionlist"/>
					<xsl:with-param name="referenceslist" select="$referenceslist"/>
					</xsl:call-template>
				</div>
				<xsl:call-template name="make-token-div"/>
			</div>
			<xsl:call-template name="make-right-div"/>
		</div>
	</body>
</xsl:template>

<xsl:template name="make-token-div">
	<xsl:if test="not(//image-section/@icons='no') and not($icons='no')">
		<div id="lldiv" class="left" style="color:white;background:#111111">
			<xsl:call-template name="make-token-buttons"/>
		</div>
	</xsl:if>
</xsl:template>

<xsl:template name="make-right-div">
	<div id="rightside" class="rightside">
		<div id="rbuttons" class="rbuttons">
			<span id="imagenav" class="imagenav">
				<input id="previmg" type="button" value="&lt;&lt;&lt;" disabled="true" onclick="loadImage(currentImage-1)"/>
				&#160;
				<span id="imagenumber" class="imagenumber"></span>
				&#160;
				<input id="nextimg" type="button" value="&gt;&gt;&gt;" disabled="true" onclick="loadImage(currentImage+1)"/>
			</span>
			<span id="selbuttons" class="selbuttons">
				<input id="annbtn" type="button" value="Annotations" disabled="true" onclick="displayAnnotation()"/>
				<input id="orgbtn" type="button" value="Original Size" disabled="true" onclick="fetchOriginal()"/>
				<input id="dcmbtn" type="button" value="Original Format" disabled="true" onclick="fetchModality(event)"/>
			</span>
		</div>
		<div id="captions" class="captions"/>
		<div id="rimage" class="rimage">
			<center id="rimagecenter">
				<!-- primary image goes here -->
			</center>
		</div>
	</div>
</xsl:template>

<xsl:template name="make-tabs">
	<xsl:param name="sectionlist"/>
	<xsl:param name="referenceslist"/>
	<div class="tabs" id="tabs">
		<xsl:call-template name="make-button">
			<xsl:with-param name="n">1</xsl:with-param>
			<xsl:with-param name="tabtitle" select="'Document'"/>
		</xsl:call-template>
		<xsl:call-template name="make-tab-buttons">
			<xsl:with-param name="n">2</xsl:with-param>
			<xsl:with-param name="nodelist" select="$sectionlist"/>
		</xsl:call-template>
		<xsl:if test="$referenceslist">
			<xsl:call-template name="make-button">
				<xsl:with-param name="n" select="2+count($sectionlist)"/>
				<xsl:with-param name="tabtitle" select="'References'"/>
			</xsl:call-template>
		</xsl:if>
	</div>
</xsl:template>

<xsl:template name="make-tab-buttons">
	<xsl:param name="n"/>
	<xsl:param name="nodelist"/>
	<xsl:if test="$nodelist">
		<xsl:call-template name="make-button">
			<xsl:with-param name="n" select="$n"/>
			<xsl:with-param name="tabtitle" select="$nodelist[position()=1]/@heading"/>
		</xsl:call-template>
		<xsl:call-template name="make-tab-buttons">
			<xsl:with-param name="n" select="$n+1"/>
			<xsl:with-param name="nodelist" select="$nodelist[position()!=1]"/>
		</xsl:call-template>
	</xsl:if>
</xsl:template>

<xsl:template name="make-button">
	<xsl:param name="n"/>
	<xsl:param name="tabtitle"/>
	<input type="button" class="u">
		<xsl:attribute name="id">b<xsl:value-of select="$n"/></xsl:attribute>
		<xsl:attribute name="onclick">
			<xsl:text>bclick('b</xsl:text>
			<xsl:value-of select="$n"/>
			<xsl:text>','tab</xsl:text>
			<xsl:value-of select="$n"/>
			<xsl:text>')</xsl:text>
		</xsl:attribute>
		<xsl:attribute name="value">
			<xsl:value-of select="$tabtitle"/>
		</xsl:attribute>
	</input>
</xsl:template>

<xsl:template name="make-section-divs">
	<xsl:param name="sectionlist"/>
	<xsl:param name="referenceslist"/>
	<div class="hide" id="tab1">
		<xsl:call-template name="page-title"/>
		<xsl:apply-templates select="author"/>
		<xsl:call-template name="page-abstract"/>
		<xsl:apply-templates select="keywords"/>
		<xsl:call-template name="document-footer"/>
	</div>

	<xsl:call-template name="make-sections">
		<xsl:with-param name="n" select="2"/>
		<xsl:with-param name="nodelist" select="$sectionlist"/>
	</xsl:call-template>

	<xsl:if test="$referenceslist">
		<div class="hide">
			<xsl:attribute name="id">
				<xsl:text>tab</xsl:text>
				<xsl:value-of select="2+count($sectionlist)"/>
			</xsl:attribute>
			<xsl:apply-templates select="$referenceslist"/>
		</div>
	</xsl:if>
</xsl:template>

<xsl:template name="make-sections">
	<xsl:param name="n"/>
	<xsl:param name="nodelist"/>
	<xsl:if test="$nodelist">
		<div class="hide">
			<xsl:attribute name="id">tab<xsl:value-of select="$n"/></xsl:attribute>
			<xsl:apply-templates select="$nodelist[position()=1]"/>
		</div>
		<xsl:call-template name="make-sections">
			<xsl:with-param name="n" select="$n+1"/>
			<xsl:with-param name="nodelist" select="$nodelist[position()!=1]"/>
		</xsl:call-template>
	</xsl:if>
</xsl:template>

<xsl:template name="make-token-buttons">
	<xsl:for-each select="//image-section/image">
		<xsl:variable name="n"><xsl:number /></xsl:variable>
		<input type="image" class="tokenbutton" width="64">
			<xsl:attribute name="src">
				<xsl:choose>
					<xsl:when test="alternative-image[@role='icon']">
						<xsl:value-of select="alternative-image[@role='icon']/@src"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="@src"/>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:attribute>
			<xsl:attribute name="onclick">
				<xsl:text>loadImage(</xsl:text>
				<xsl:value-of select="$n"/>
				<xsl:text>)</xsl:text>
			</xsl:attribute>
			<xsl:attribute name="title">
				<xsl:value-of select="$n"/>
			</xsl:attribute>
		</input>
		<xsl:text>&#160;</xsl:text>
	</xsl:for-each>
</xsl:template>

<xsl:template name="tab-title">
	<h1><xsl:value-of select="title"/></h1>
</xsl:template>

<xsl:template name="page-title">
	<xsl:choose>
		<xsl:when test="$unknown='yes'">
			<xsl:choose>
				<xsl:when test="alternative-title">
					<h1><xsl:value-of select="alternative-title"/></h1>
				</xsl:when>
				<xsl:otherwise>
					<xsl:choose>
						<xsl:when test="//category">
							<h1>Unknown - <xsl:value-of select="//category"/></h1>
						</xsl:when>
						<xsl:otherwise>
							<h1>Unknown</h1>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>
			<h1><xsl:value-of select="title"/></h1>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template match="author">
	<p class="authorname">
		<xsl:apply-templates/>
	</p>
</xsl:template>

<xsl:template match="name">
	<xsl:value-of select="."/>
</xsl:template>

<xsl:template match="affiliation">
	<p class="authorinfo">
	<xsl:value-of select="."/>
	</p>
</xsl:template>

<xsl:template match="contact">
	<p class="authorinfo">
		<xsl:value-of select="."/>
	</p>
</xsl:template>

<xsl:template name="page-abstract">
	<xsl:choose>
		<xsl:when test="$unknown='yes'">
			<xsl:apply-templates select="alternative-abstract"/>
		</xsl:when>
		<xsl:otherwise>
			<xsl:apply-templates select="abstract"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template match="abstract | alternative-abstract">
	<h2>Abstract</h2>
	<xsl:apply-templates/>
</xsl:template>

<xsl:template match="keywords">
	<xsl:param name="kws"><xsl:value-of select="."/></xsl:param>
	<xsl:if test="string-length(normalize-space($kws)) &gt; 0">
		<h2>Keywords</h2>
		<p>
			<xsl:apply-templates select="term | text()"/>
		</p>
	</xsl:if>
</xsl:template>

<xsl:template match="section">
	<xsl:if test="not(count(*)=0)">
		<h2><xsl:value-of select="@heading"/></h2>
		<xsl:apply-templates/>
	</xsl:if>
</xsl:template>

<xsl:template match="image-section">
	<xsl:if test="$display='tab'">
		<div id="leftside" class="leftside">
			<xsl:call-template name="make-token-div"/>
		</div>
		<xsl:call-template name="make-right-div"/>
	</xsl:if>
	<xsl:if test="$display='page' and not(count(image) = 0)">
		<h2><xsl:value-of select="@heading"/></h2>
		<center>
			<xsl:for-each select="image">
				<img><xsl:copy-of select="@src"/></img>
				<br/>
				<xsl:choose>
					<xsl:when test="image-caption">
						<xsl:if test="image-caption[not(@display) or @display='always']">
							<p class="centeredcaption">
								<xsl:value-of select="normalize-space(image-caption[not(@display) or @display='always'])"/>
							</p>
						</xsl:if>
						<xsl:if test="image-caption[@display='click']">
							<p class="centeredcaption">
								<xsl:value-of select="normalize-space(image-caption[@display='click'])"/>
							</p>
						</xsl:if>
						<br/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:text>&#160;</xsl:text>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</center>
	</xsl:if>
</xsl:template>

<xsl:template match="references">
	<h2>References</h2>
	<xsl:if test="not(count(reference) = 0)">
		<ol>
			<xsl:for-each select="reference">
				<li><xsl:apply-templates/></li>
			</xsl:for-each>
		</ol>
	</xsl:if>
</xsl:template>

<xsl:template match="reference">
	<xsl:apply-templates/>
</xsl:template>

<xsl:template name="document-footer">
	<hr class="docfooter" />
	<p class="center">
		<xsl:if test="rights">
			<xsl:value-of select="rights"/>
			<br/>
		</xsl:if>
		<xsl:if test="publication-date">
			Publication Date: <xsl:value-of select="publication-date"/>
			<br/><br/>
		</xsl:if>
		<xsl:choose>
			<xsl:when test="$preview='yes'">
				Document Preview
			</xsl:when>
			<xsl:otherwise>
				<table class="footer" rules="cols">
					<tr>
						<th>Document</th>
						<th>Image Export</th>
					</tr>
					<tr>
						<td><xsl:call-template name="edit-button"/></td>
						<td><xsl:call-template name="zip-phi-button"/></td>
					</tr>
					<tr>
						<td><xsl:call-template name="publish-button"/></td>
						<td><xsl:call-template name="zip-no-phi-button"/></td>
					</tr>
					<tr>
						<td><xsl:call-template name="export-button"/></td>
						<td><xsl:call-template name="dicom-button"/></td>
					</tr>
					<tr>
						<td><xsl:call-template name="delete-button"/></td>
						<td><xsl:call-template name="database-button"/></td>
					</tr>
					<xsl:call-template name="saveimages-button"/>
					<xsl:call-template name="addimages-button"/>
					<xsl:if test="$user-is-admin = 'yes'">
						<tr rules="rows">
							<td colspan="2"><xsl:call-template name="caseoftheday-button"/></td>
						</tr>
					</xsl:if>
					<xsl:call-template name="url-button"/>
				</table>
			</xsl:otherwise>
		</xsl:choose>
	</p>
</xsl:template>

<xsl:template name="url-button">
		<tr rules="rows">
			<td colspan="2">
				<input type="button" value="Document URL"
						title="Show the document URL"
						onclick="copyTextToClipboard('{$doc-url}'); alert('{$doc-url}');"/>
			</td>
		</tr>
</xsl:template>

<xsl:template name="saveimages-button">
	<xsl:if test="string-length($filecabinet-url)!=0">
		<tr rules="rows">
			<td colspan="2">
				<input type="button" value="Save Images to File Cabinet"
						title="Save the images in this document to my File Cabinet">
					<xsl:attribute name="onclick">
						<xsl:text>saveImages('</xsl:text>
						<xsl:value-of select="$filecabinet-url"/>
						<xsl:text>?path=</xsl:text>
						<xsl:value-of select="$doc-path"/>
						<xsl:text>');</xsl:text>
					</xsl:attribute>
				</input>
			</td>
		</tr>
	</xsl:if>
</xsl:template>

<xsl:template name="addimages-button">
	<xsl:if test="$edit-url and (//insert-image or //insert-megasave)">
		<td colspan="2">
			<input type="button" value="Add Images to This Document">
				<xsl:attribute name="onclick">
					<xsl:text>openURL('</xsl:text>
					<xsl:value-of select="$add-url"/>
					<xsl:text>','_self');</xsl:text>
				</xsl:attribute>
			</input>
		</td>
	</xsl:if>
</xsl:template>

<xsl:template name="caseoftheday-button">
	<xsl:variable name="ttl">
		<xsl:value-of select="normalize-space(title)"/>
	</xsl:variable>
	<xsl:variable name="abs">
		<xsl:value-of select="concat(substring(normalize-space(abstract),1,200),'...')"/>
	</xsl:variable>
	<xsl:variable name="remove">"'</xsl:variable>
	<xsl:variable name="image-ref">
		<xsl:value-of select="//image/@src"/>
	</xsl:variable>
	<input type="button"
		value="Case of the Day"
		title="Install this document as the Case of the Day"
		onclick="setCaseOfTheDay();"/>

			<input id="codurl" type="hidden" value="{$server-url}/mirc/news/add"/>
			<input id="codtitle" type="hidden" value="{translate($ttl,$remove,'')}"/>
			<input id="coddesc" type="hidden" value="{translate($abs,$remove,'')}"/>
			<input id="codlink" type="hidden" value="{$doc-url}"/>
			<input id="codimage" type="hidden">
				<xsl:attribute name="value">
					<xsl:if test="not(string-length($image-ref) = 0)">
						<xsl:value-of select="$server-url"/>
						<xsl:value-of select="$context-path"/>
						<xsl:value-of select="$dir-path"/>
						<xsl:value-of select="$image-ref"/>
					</xsl:if>
				</xsl:attribute>
			</input>

</xsl:template>

<xsl:template name="export-button">
	<input type="button" value="Export" title="Export this document">
		<xsl:choose>
			<xsl:when test="string-length($export-url)!=0">
				<xsl:attribute name="onclick">
					<xsl:text>exportZipFile('</xsl:text>
					<xsl:value-of select="$export-url"/>
					<xsl:text>','_self', event);</xsl:text>
				</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="disabled">true</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</input>
</xsl:template>

<xsl:template name="zip-phi-button">
	<input type="button" value="Zip (with PHI)"
		title="Export the identified study dataset">
		<xsl:choose>
			<xsl:when test="//insert-dataset[@phi='yes']">
				<xsl:attribute name="onclick">
					<xsl:text>openURL('</xsl:text>
					<xsl:value-of select="$export-url"/>
					<xsl:text>=phi</xsl:text>
					<xsl:text>','_self');</xsl:text>
				</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="disabled">true</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</input>
</xsl:template>

<xsl:template name="edit-button">
	<input type="button" value="Edit" title="Edit this document">
		<xsl:choose>
			<xsl:when test="string-length($edit-url)!=0">
				<xsl:attribute name="onclick">
					<xsl:text>openURL('</xsl:text>
					<xsl:value-of select="$edit-url"/>
					<xsl:text>','_blank');</xsl:text>
				</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="disabled">true</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</input>
</xsl:template>

<xsl:template name="zip-no-phi-button">
	<input type="button" value="Zip (without PHI)"
		title="Export the de-identified study dataset">
		<xsl:choose>
			<xsl:when test="//insert-dataset[not(@phi='yes')]">
				<xsl:attribute name="onclick">
					<xsl:text>openURL('</xsl:text>
					<xsl:value-of select="$export-url"/>
					<xsl:text>=no-phi</xsl:text>
					<xsl:text>','_self');</xsl:text>
				</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="disabled">true</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</input>
</xsl:template>

<xsl:template name="publish-button">
	<input type="button" value="Publish" title="Publish this document">
		<xsl:choose>
			<xsl:when test="string-length($publish-url)!=0 and
							not(contains(authorization/read,'*'))">
				<xsl:attribute name="onclick">
					<xsl:text>openURL('</xsl:text>
					<xsl:value-of select="$publish-url"/>
					<xsl:text>','_blank');</xsl:text>
				</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="disabled">true</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</input>
</xsl:template>

<xsl:template name="dicom-button">
	<input type="button" value="To DICOM SCP"
		title="Queue the DICOM objects from this document for the DicomExportService">
		<xsl:choose>
			<xsl:when test="string-length($dicom-export-url)!=0">
				<xsl:attribute name="onclick">
					<xsl:text>openURL('</xsl:text>
					<xsl:value-of select="$dicom-export-url"/>
					<xsl:text>','_blank');</xsl:text>
				</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="disabled">true</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</input>
</xsl:template>

<xsl:template name="delete-button">
	<input type="button" value="Delete"
		title="Delete this document from the server">
		<xsl:choose>
			<xsl:when test="string-length($delete-url)!=0">
				<xsl:attribute name="onclick">
					<xsl:text>deleteDocument('</xsl:text>
					<xsl:value-of select="$delete-url"/>
					<xsl:text>');</xsl:text>
				</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="disabled">true</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</input>
</xsl:template>

<xsl:template name="database-button">
	<input type="button" value="To Database"
		title="Queue the DICOM objects from this document for the DatabaseExportService">
		<xsl:choose>
			<xsl:when test="string-length($database-export-url)!=0">
				<xsl:attribute name="onclick">
					<xsl:text>openURL('</xsl:text>
					<xsl:value-of select="$database-export-url"/>
					<xsl:text>','_blank');</xsl:text>
				</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="disabled"></xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</input>

</xsl:template>

<xsl:template match="a">
	<xsl:element name="a">
		<xsl:if test="href">
			<xsl:attribute name="href">
				<xsl:call-template name="escape">
					<xsl:with-param name="text" select="normalize-space(href)"/>
				</xsl:call-template>
			</xsl:attribute>
		</xsl:if>
		<xsl:apply-templates select="*[not('href')] | @* | text()"/>
	</xsl:element>
</xsl:template>

<xsl:template match="iframe">
	<xsl:element name="iframe">
		<xsl:choose>
			<xsl:when test="$display='page'">
				<xsl:if test="src">
					<xsl:attribute name="src">
						<xsl:call-template name="escape">
							<xsl:with-param name="text" select="normalize-space(src)"/>
						</xsl:call-template>
					</xsl:attribute>
					<xsl:apply-templates select="*[not('src')] | @* | text()"/>
				</xsl:if>
				<xsl:if test="not(src)">
					<xsl:apply-templates select="* | @* | text()"/>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates select="@*[not(name()='src')]"/>
				<xsl:attribute name="src"></xsl:attribute>
				<xsl:if test="src">
					<xsl:attribute name="longdesc">
						<xsl:call-template name="escape">
							<xsl:with-param name="text" select="normalize-space(src)"/>
						</xsl:call-template>
					</xsl:attribute>
				</xsl:if>
				<xsl:if test="not(src)">
					<xsl:attribute name="longdesc"><xsl:value-of select="@src"/></xsl:attribute>
				</xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:element>
</xsl:template>

<xsl:template name="escape">
	<xsl:param name="text"/>
	<xsl:choose>
		<xsl:when test="contains($text,'|')">
			<xsl:value-of select="substring-before($text,'|')"/>
			<xsl:text>&amp;</xsl:text>
			<xsl:call-template name="escape">
				<xsl:with-param name="text" select="substring-after($text,'|')"/>
			</xsl:call-template>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="$text"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template match="image">
	<xsl:if test="@src">
		<xsl:element name="img">
			<xsl:apply-templates select="@src | @width | @height"/>
		</xsl:element>
	</xsl:if>
	<xsl:if test="@href">
		<xsl:element name="a">
			<xsl:apply-templates select="@href"/>
			<xsl:apply-templates/></xsl:element>
	</xsl:if>
</xsl:template>

<xsl:template match="show">
	<xsl:if test="$display='mstf' or $display='mirctf'">
		<xsl:if test="@image | @annotation">
			<xsl:choose>
				<xsl:when test="@image">
					<input type="button" value="&gt;">
						<xsl:attribute name="onclick">
							<xsl:text>loadImage(</xsl:text>
							<xsl:value-of select="@image"/>
							<xsl:text>)</xsl:text>
						</xsl:attribute>
					</input>
				</xsl:when>
				<xsl:when test="@annotation">
					<input type="button" value="&gt;">
						<xsl:attribute name="onclick">
							<xsl:text>loadannotation(</xsl:text>
							<xsl:value-of select="@annotation"/>
							<xsl:text>)</xsl:text>
							</xsl:attribute>
					</input>
				</xsl:when>
			</xsl:choose>
		</xsl:if>
	</xsl:if>
</xsl:template>

<xsl:template match="text">
	<xsl:if test="not(@visible='no') and not((@visible='owner') and not($user-is-owner='yes'))">
		<xsl:apply-templates/>
	</xsl:if>
</xsl:template>

<xsl:template match="text-caption">
	<xsl:if test="not(@visible='no')">
	<xsl:variable name="text-to-show" select="not(string-length(normalize-space(.))=0)"/>
		<xsl:if test="$text-to-show or @display='always'">
			<xsl:if test="$text-to-show and not(@jump-buttons='yes')">
				<br/><xsl:apply-templates/><br/>
			</xsl:if>
			<xsl:if test="@jump-buttons='yes'">
				<br/>
				<div>
					<xsl:if test="$text-to-show and @show-button='yes'">
						<xsl:attribute name="class">
							<xsl:text>hide</xsl:text>
						</xsl:attribute>
					</xsl:if>
					<xsl:if test="$text-to-show">
						<xsl:apply-templates/>
					</xsl:if>
				</div>
				<br/>
				<input onclick="jumpButton(-1, event);">
					<xsl:attribute name="type">
						<xsl:value-of select="$input-type"/>
					</xsl:attribute>
					<xsl:if test="$input-type='image'">
						<xsl:attribute name="src">
							<xsl:value-of select="$context-path"/>
							<xsl:text>/buttons/back.png</xsl:text>
						</xsl:attribute>
					</xsl:if>
					<xsl:if test="$input-type='button'">
						<xsl:attribute name="value">
							<xsl:text>&lt;&lt;&lt;</xsl:text>
						</xsl:attribute>
					</xsl:if>
				</input>
				<xsl:if test="$text-to-show and @show-button='yes'">
					<input onclick="showButton(event);">
						<xsl:attribute name="type">
							<xsl:value-of select="$input-type"/>
						</xsl:attribute>
						<xsl:if test="$input-type='image'">
							<xsl:attribute name="src">
								<xsl:value-of select="$context-path"/>
								<xsl:text>/buttons/showcaption.png</xsl:text>
							</xsl:attribute>
						</xsl:if>
						<xsl:if test="$input-type='button'">
							<xsl:attribute name="value">
								<xsl:text>Show Caption</xsl:text>
							</xsl:attribute>
						</xsl:if>
					</input>
				</xsl:if>
				<input onclick="jumpButton(1, event);">
					<xsl:attribute name="type">
						<xsl:value-of select="$input-type"/>
					</xsl:attribute>
					<xsl:if test="$input-type='image'">
						<xsl:attribute name="src">
							<xsl:value-of select="$context-path"/>
							<xsl:text>/buttons/forward.png</xsl:text>
						</xsl:attribute>
					</xsl:if>
					<xsl:if test="$input-type='button'">
						<xsl:attribute name="value">
							<xsl:text>&gt;&gt;&gt;</xsl:text>
						</xsl:attribute>
					</xsl:if>
				</input>
				<br/>
			</xsl:if>
			<br/>
		</xsl:if>
	</xsl:if>
</xsl:template>

<xsl:template match="insert-megasave | insert-image | insert-kin |
                     insert-document-reference | insert-note | insert-user |
                     insert-date | insert-time | phi | block" />

<xsl:template match="history | findings | diagnosis | confirmation |
                     differential-diagnosis | discussion |
                     pathology | anatomy | organ-system | modality | rights |
                     publication-date | format | compression |
                     document-type | category | level | lexicon | access |
                     language | creator | document-id">
	<xsl:if test="not(@visible='no')">
		<xsl:apply-templates/>
	</xsl:if>
</xsl:template>

<xsl:template match="years">
	<xsl:apply-templates/>
	<xsl:text> years</xsl:text>
</xsl:template>

<xsl:template match="months">
	<xsl:apply-templates/>
	<xsl:text> months</xsl:text>
</xsl:template>

<xsl:template match="weeks">
	<xsl:apply-templates/>
	<xsl:text> weeks</xsl:text>
</xsl:template>

<xsl:template match="days">
	<xsl:apply-templates/>
	<xsl:text> days</xsl:text>
</xsl:template>

<xsl:template match="code">
	<xsl:if test="not(@visible='no')">
		<xsl:if test="@coding-system">
			<xsl:value-of select="@coding-system"/>
			<xsl:text>:</xsl:text>
		</xsl:if>
		<xsl:value-of select="text()"/>
		<xsl:apply-templates select="meaning"/>
	</xsl:if>
</xsl:template>

<xsl:template match="meaning">
	<xsl:if test="not(@visible='no')">
		(<xsl:apply-templates/>)
	</xsl:if>
</xsl:template>

<xsl:template match="term">
	<xsl:choose>
		<xsl:when test="@lexicon='RadLex'">
			<a class="term" href="http://radlex.org/{@id}" target="radlex">
				<xsl:apply-templates/>
			</a>
		</xsl:when>
		<xsl:otherwise>
			<xsl:apply-templates/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template match="revision-history">
	<xsl:if test="not(@visible='no')">
		<center>
			<table border="1" width="80%">
				<thead>
					<td><b>Author</b></td>
					<td><b>Date</b></td>
					<td width="60%"><b>Description</b></td>
				</thead>
				<xsl:apply-templates select="revision"/>
			</table>
		</center>
	</xsl:if>
</xsl:template>

<xsl:template match="revision">
	<tr>
		<td><xsl:apply-templates select="revision-author"/></td>
		<td><xsl:apply-templates select="revision-date"/></td>
		<td><xsl:apply-templates select="revision-description"/></td>
	</tr>
</xsl:template>

<xsl:template match="peer-review">
	<xsl:if test="not(@visible='no')">
		<p>
			<b>Peer Review Status</b><br />
			<table>
				<tr><td>Approval date:</td><td><xsl:apply-templates select="approval-date"/></td></tr>
				<tr><td>Expiration date:</td><td><xsl:apply-templates select="expiration-date"/></td></tr>
				<tr><td>Reviewing authority:</td><td><xsl:apply-templates select="reviewing-authority"/></td></tr>
				<tr><td>Reviewer:</td><td><xsl:apply-templates select="reviewer"/></td></tr>
			</table>
		</p>
	</xsl:if>
</xsl:template>

<xsl:template match="patient">
	<xsl:variable name="show-phi"
				  select="(@visible!='phi-restricted') or ($user-is-owner='yes')"/>
	<table class="pttable" align="center">
		<xsl:if test="pt-name and $show-phi">
			<tr>
				<td class="ptlabel">Name:</td>
				<td><xsl:value-of select="normalize-space(pt-name)"/></td>
			</tr>
		</xsl:if>
		<xsl:if test="pt-id and $show-phi">
			<tr>
				<td class="ptlabel">ID:</td>
				<td><xsl:value-of select="normalize-space(pt-id)"/></td>
			</tr>
		</xsl:if>
		<xsl:if test="pt-mrn and $show-phi">
			<tr>
				<td class="ptlabel">MRN:</td>
				<td><xsl:value-of select="normalize-space(pt-mrn)"/></td>
			</tr>
		</xsl:if>
		<xsl:if test="pt-age">
			<tr>
				<td class="ptlabel">Age:</td>
				<td>
					<xsl:if test="pt-age/years">
						<xsl:value-of select="pt-age/years"/>
						<xsl:text>&#160;years </xsl:text>
					</xsl:if>
					<xsl:if test="pt-age/months">
						<xsl:value-of select="pt-age/months"/>
						<xsl:text>&#160;months </xsl:text>
					</xsl:if>
					<xsl:if test="pt-age/weeks">
						<xsl:value-of select="pt-age/weeks"/>
						<xsl:text>&#160;weeks </xsl:text>
					</xsl:if>
					<xsl:if test="pt-age/days">
						<xsl:value-of select="pt-age/days"/>
						<xsl:text>&#160;days </xsl:text>
					</xsl:if>
				</td>
			</tr>
		</xsl:if>
		<xsl:if test="pt-sex">
			<tr>
				<td class="ptlabel">Sex:</td>
				<td><xsl:value-of select="normalize-space(pt-sex)"/></td>
			</tr>
		</xsl:if>
		<xsl:if test="pt-race">
			<tr>
				<td class="ptlabel">Race:</td>
				<td><xsl:value-of select="normalize-space(pt-race)"/></td>
			</tr>
		</xsl:if>
		<xsl:if test="pt-species">
			<tr>
				<td class="ptlabel">Species:</td>
				<td><xsl:value-of select="normalize-space(pt-species)"/></td>
			</tr>
		</xsl:if>
		<xsl:if test="pt-breed">
			<tr>
				<td class="ptlabel">Breed:</td>
				<td><xsl:value-of select="normalize-space(pt-breed)"/></td>
			</tr>
		</xsl:if>
	</table>
</xsl:template>

<xsl:template match="metadata-refs">
	<xsl:if test="metadata">
		<table class="mdtable">
			<xsl:apply-templates select="metadata"/>
		</table>
	</xsl:if>
</xsl:template>

<xsl:template match="metadata">
	<tr>
		<td>
			<input type="button">
				<xsl:attribute name="value">
					<xsl:value-of select="type"/>
				</xsl:attribute>
				<xsl:attribute name="onclick">
					<xsl:text>fetchFile('</xsl:text>
					<xsl:value-of select="@href"/>
					<xsl:text>', event);</xsl:text>
				</xsl:attribute>
			</input>
			<br/>
			<xsl:value-of select="date"/>
		</td>
		<td><xsl:value-of select="desc"/></td>
	</tr>
</xsl:template>

<xsl:template match="quiz">
	<p><xsl:apply-templates select="quiz-context"/></p>
	<ol>
		<xsl:apply-templates select="question"/>
	</ol>
</xsl:template>

<xsl:template match="quiz-context | question-body">
	<xsl:apply-templates/>
</xsl:template>

<xsl:template match="question">
	<li>
		<xsl:apply-templates select="question-body"/>
		<p>
			<table border="1" width="80%">
				<xsl:apply-templates select="answer"/>
			</table>
		</p>
	</li>
</xsl:template>

<xsl:template match="answer">
	<xsl:param name="a">RspDiv<xsl:number format="1" level="any"/></xsl:param>
	<tr>
		<td align="center" valign="top" width="10%">
			<button style="width:40">
				<xsl:attribute name="onclick">
					<xsl:text>toggleResponse('</xsl:text>
					<xsl:value-of select="$a"/>
					<xsl:text>')</xsl:text>
				</xsl:attribute>
				<xsl:number format="a"/>
			</button>
		</td>
		<td>
			<xsl:apply-templates select="answer-body"/>
			<xsl:apply-templates select="response">
				<xsl:with-param name="a" select="$a"/>
			</xsl:apply-templates>
		</td>
	</tr>
</xsl:template>

<xsl:template match="answer-body">
	<div>
		<xsl:apply-templates/>
	</div>
</xsl:template>

<xsl:template match="response">
	<xsl:param name="a"/>
	<div class="hide">
		<xsl:attribute name="id">
			<xsl:value-of select="$a"/>
		</xsl:attribute>
		<hr/>
		<xsl:apply-templates/>
	</div>
</xsl:template>

<xsl:template name="script-init">
	<script>
		var contextPath = '<xsl:value-of select="$context-path"/>';
		var dirPath = '<xsl:value-of select="$dir-path"/>';
		var display = '<xsl:value-of select="$display"/>';
		var inputType = '<xsl:value-of select="$input-type"/>';
		var firstTab = <xsl:choose>
			<xsl:when test="@first-tab"><xsl:value-of select="@first-tab"/></xsl:when>
			<xsl:otherwise>2</xsl:otherwise>
		</xsl:choose>;
		var showEmptyTabs = '<xsl:value-of select="@show-empty-tabs"/>';
		var background = '<xsl:value-of select="$bgcolor"/>';

		<xsl:if test="//image-section">
			var imageSection = 'yes';

			var imagePaneWidth = <xsl:choose>
				<xsl:when test="//image-section/@image-pane-width">
					<xsl:value-of select="//image-section/@image-pane-width"/>;
				</xsl:when>
				<xsl:otherwise>700;</xsl:otherwise>
			</xsl:choose>

			var images = new Array(
			<xsl:for-each select="//image-section/image">
				new Array(
					"<xsl:value-of select="@width"/>",
					"<xsl:value-of select="@src"/>",
					"<xsl:value-of select="alternative-image
						[@role='annotation' and (@type='svg' or (not(@type) and contains(@src,'.svg')))]/@src"/>",
					"<xsl:value-of select="alternative-image
						[@role='annotation' and (@type='image' or (not(@type) and not(contains(@src,'.svg'))))]/@src"/>",
					"<xsl:value-of select="alternative-image[@role='original-dimensions']/@src"/>",
					"<xsl:value-of select="alternative-image[@role='original-format']/@src"/>",
					"<xsl:value-of select="normalize-space(image-caption[not(@display) or @display='always'])"/>",
					"<xsl:value-of select="normalize-space(image-caption[@display='click'])"/>",
					0
				)<xsl:if test="position()!=last()">,</xsl:if>
			</xsl:for-each>
			);
		</xsl:if>

		<xsl:if test="not(//image-section)">var imageSection = 'no';
		</xsl:if>
	</script>
	<xsl:text>
</xsl:text>
</xsl:template>

</xsl:stylesheet>
