#Please complete the following
#•	Automate the below acceptance test
#•	Can you think of any other scenarios? if so write them and if you can, automate them
#•	Ensure you check the completed code into git and send us the link to the git repo (please register a public git account if you don't have one)
#•	Once completed we will mark and get back to you to potentially arrange a further interview.
#•	The only timebox is that you complete it before tomorrow, take as long as you need (gives you an opportunity to clean up your code if you can or is needed)
#•	Any bugs? let us know for bonus points!
#The site is QAWorks.com and here is the feature:

# ************************************************************************************************************************
# *********** NOTE for the IE Driver to work then all Internet Zones must have the same Protected Mode Setting ***********
# *********** See: InternetExplorer> Internet Options> Security> Enable Protected Mode                         ***********
# For IE 11 only, you will need to set a registry entry on the target computer so that the driver can maintain a 
# connection to the instance of Internet Explorer it creates. 
# 
# For 32-bit Windows installations, the key you must examine in the registry editor is:
# HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BFCACHE 
# 
# For 64-bit Windows installations, the key is:
# HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BFCACHE
# 
# Please note that the FEATURE_BFCACHE subkey may or may not be present, and should be created if it is not present. 
# Important: Inside this key, create a DWORD value named iexplore.exe with the value of 0.
# ************************************************************************************************************************
Feature: Contact Us Page
  As an end user
  I want a contact us page
  So that I can find out more about QAWorks exciting services!!

  @ContactPage
  Scenario: Valid Submission
    Given I am on the QAWorks Site
    Then I should be able to contact QAWorks with the following information
      | FieldName | value                                     |
      | name      | j.Bloggs                                  |
      | email     | j.Bloggs@qaworks.com                      |
      | message   | please contact me I want to find out more |
	Then The page title changes to 'QAWorks'


  Scenario: Invalid Email Submission 
    Given I am on the QAWorks Site
    When I try to contact QAWorks with the following information
      | FieldName | value                                     |
      | name      | j.Bloggs                                  |
      | email     | j.Bloggs.qaworks.com                      |
      | message   | please contact me I want to find out more |
	Then I get the validation errors
      | FieldName                      | value                 |
      | email_Validation_WellFormatted | Invalid Email Address |
	  
Scenario: Missing Email Submission 
    Given I am on the QAWorks Site
    When I try to contact QAWorks with the following information
      | FieldName | value                                     |
      | name      | j.Bloggs                                  |
      | email     |                                           |
      | message   | please contact me I want to find out more |
	Then I get the validation errors
      | FieldName                | value                        |
      | email_Validation_Missing | An Email address is required |

Scenario: Missing Name Submission 
    Given I am on the QAWorks Site
    When I try to contact QAWorks with the following information
      | FieldName | value                                     |
      | name      |                                           |
      | email     | j.Bloggs.qaworks.com                      |
      | message   | please contact me I want to find out more |
	Then I get the validation errors
      | FieldName               | value                 |
      | name_Validation_Missing | Your name is required |

Scenario: Missing Message Submission 
    Given I am on the QAWorks Site
    When I try to contact QAWorks with the following information
      | FieldName | value                |
      | name      | j.Bloggs             |
      | email     | j.Bloggs.qaworks.com |
      | message   |                      |
	Then I get the validation errors
      | FieldName                  | value                    |
      | message_Validation_Missing | Please type your message |


 # Scenario: Invalid Email Submission 
 #   Given I am on the QAWorks Site
 #   When I try to contact QAWorks with the following information
 #     | FieldName | value                                     |
 #     | name      | j.Bloggs                                  |
 #     | email     | j.Bloggs.qaworks.com                      |
 #     | message   | please contact me I want to find out more |
	#Then I get the validation errors
 #     | FieldName                      | value                        |
 #     | email_Validation_WellFormatted | Invalid Email Address        |
 #     | message_Validation_Missing     | Please type your message     |
 #     | emil_Validation_Missing        | An Email address is required |
 #     | name_Validation_Missing        | Your name is required        |