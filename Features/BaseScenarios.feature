Feature: BaseScenarios

A short summary of the feature

@tag1
Scenario: Create a new Word document

    Given I click on Blank Document from the Home menu

    Then the default font is ‘Aptos (Body)’



Scenario: Create and save a document using Save as from the File menu

    Given I create a new document and paste in 100 characters

    When I save the file onto the desktop with a random name

    Then the document will be created and saved in the desktop directory



Scenario: Print Word document to PDF

    Given I create a new document and paste in 100 characters

    When I save document as a pdf document to the desktop

    Then a PDF file will be created in the desktop directory

    And the PDF file will contain the correct text


