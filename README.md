# JSONCOnverter -> JSON Changenote to HTML Converter

![JSONCOnverter](SalesPitch.png)

## Notes/Know Bugs:

> Files that do not follow the format posted below will crash the application

## Features(Planned In Brackets)

File selection.

Simple UI

Automatic conversion and execution of JSON to HTML

## View of progress

Main Build : 

![JSONCOnverter](SalesPitch.png)


## Latest Build

*08/05/2021 - Main Build*

## Latest Update Notes:

![JSONCOnverter](SalesPitch.png)

Main Build


To Sum Up:
	Add File-opening functionality.
	Add JSON reading method.
	Add HTML conversion method.
	Add GUI Interface

*THE BELOW CODE CAN BE DOWNLAODED AS A JSON FILE AND RUN WITH ![THIS PROGRAM](https://github.com/StarshipladDev/JSONConverter)*

```
	{
	"title": "JSONChangeNotesReaderApp",
	"summary": "Add core program",
	"updates": [
		{
			"date": "06/05/2021",
			"updateNotes": [
				{
					"area": "JSONChangeNotesReaderApp - > Models",
					"changes": [
						"Add 3 Models for Deserializing JSON"
					]
				},
				{
					"area": "JSONChangeNotesReaderApp -> Form1.cs ",
					"changes": [
						"Add Methods to Deserialize JSON , convert strings to HTML with inline CSS, and write/execute HTML files for core reason",
						"Add 'Background.png' to GUI for aesthetics",
						"Add GUI  500x500 with label stating file location and buttons to select file and convert JSON"
					]
				}
			],
			"issuesEncountered": {
				"issue": {
					"none, went perfectly"
				}
			}
		}
	],
	"toDo": [ "Completed for the time being" ]
}
	
```

## Latest Updates

## Next Build ([ ] -Not done , [0] - Half Done , [x] - Done)

Utility Acheived - No excpected Updates


## Skill developing

I planned on this project improving my skills in the following:

>Understanding of 3-D representation

>Proof of skill development since ![Gun Run](https://github.com/StarshipladDev/GunRun)

>C# Code practice

>Correct SDLC practice

>Pixel Art and Animation

## Installing and Compiling:

Download and run 'JSOnConverter.exe' , then press the btton to select a .JSON file.

Press 'Convert JSON' to convert that file to an HTML page and automatically run it in your PC's browser.

*Please note :*

JSON files you input must follow the below format (Comments following '#'):

```
	{
		
	"title": "Title of updates",
	
	"summary": "summary of changes",
	
	*Each 'updates' element is the changes that occured on the each date*
	
	*Sections in '[' ']' brackets are mutable*
	
	"updates": [
		{
			"date": ["Date 1"],
			"updateNotes": [
				{
					"area": ["System -> Class -> Method"],
					"changes": [
						["Cange 1"],
						["Change 2"],
					]
				},
				{
					"area": ["System -> Class -> Method"],
					"changes": [
						["Cange 1"],
						["Change 2"],
						["Change 3"]
					]
				}
			],
			"issuesEncountered": {
				"issue": [
					[Issue1"],["Issue2"]
				]
			}
		},
		{
			"date": ["Date 2"],
			"updateNotes": [
				{
					"area": ["System -> Class -> Method"],
					"changes": [
						["Cange 1"],
						["Change 2"],
					]
				},
				{
					"area": ["System -> Class -> Method"],
					"changes": [
						["Cange 1"],
					]
				}
			],
			"issuesEncountered": {
				"issue": [
					[Issue1"]
				]
			}
		}
	],
	"toDo": [ ["TODO1"] ,["TODO2"]]
}
	
```


