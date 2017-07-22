Welcome to **StoryLine.Wiremock** examples. This folder contains the following solutions:
* **Microservice.Gateway** contains implementation of simple REST microservice which provides proxy\routing logic for user and document functionality. 
* **Microservice.Gateway.Subsys** contains subsystem tests written with help of **StoryLine.Rest** and **StoryLine.Wiremock** libraries. 

**NOTE**: You may need to install [Docker](https://www.docker.com/) in order to run Wiremock tool. Alternatively you may install Wiremock as stand alone application but this approach may be more complicated.

In order to run tests successfully the follection actions must be performed:
* Build **Microservice.Gateway** solution.
* Run **Microservice.Gateway** microservice.
* Create Docker container running Wiremock. Use `run-wiremock.bat` located in root of Examples folder.
* Build **Microservice.Gateway.Subsys** solution.
* Run tests included in **Microservice.Gateway.Subsys**.

Subsystem tests performs validation proxying\roting logic implemented by gateway microservice. Tests attempt invoke all operations and verify the results match expectations.