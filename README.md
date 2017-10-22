# Google Cloud Datastore Example

## Introduction

This is a small .NET Core 2.0 console application which demonstrates how to read and write data using Google Cloud Datastore.

The aim is to show how to write a complex object to cloud datastore including single child objects and array's of child objects.

The example focuses around Projects which have an Owner and contain a list of Task.

## Getting it running

You'll need to have .NET Core 2 installed and a GCP account. The app uses the default GCP credentials which will need set through the cloud SDK shell (blog post on this is coming soon). 

The application accepts two arguments:

1. The GCP project ID the cloud datastore instance exists in - required
2. The namespace to write to within cloud datastore - optional

The first argument is required, the second is optional. If argument two is not provided the default namespace in cloud datastore will be used.

Note although it should work on Linux/Mac it's only been tested on Windows.

## Limitations

The data written to datastore is currently hard coded within the application. There are currently no plans to change that however it would be easy enough to wire the same code into an API or web app.

All code is provided without warranty or guarantee.
