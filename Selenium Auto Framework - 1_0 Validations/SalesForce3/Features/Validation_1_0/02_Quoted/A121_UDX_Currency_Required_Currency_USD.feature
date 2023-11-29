Feature: A121_UDX_Currency_Required_Currency_USD

@Sanity @Regression
	Scenario:  If issuing or producing office is in Santiago then picklist must be Yes or No - Required @ working stage; if yes then Submission Currency must equal USD 1.0

	#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

	## User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Quoted'

	# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

	# Stage progression from proxpect to working
	Then User Selected the Stage as 'Working' from View Mode and Clicked on Mark as current stage button
	Then User Verified submission record status changed to "Working"

	# Validation : A121_UDX_Currency_Required_Currency_USD
	Then User Clicked on "Edit" button
	Then User Updated 'Submission Currency' field with values 'AED - UAE Dirham'
	Then User click on Save button 
	Then Verify the Error message "Please confirm if the currency is UDX or not, if yes, Submission Currency must be USD ( Submission Details Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

	# Stage progression from Working to Quoted
	Then User Selected the Stage as 'Quoted' from View Mode and Clicked on Mark as current stage button
	Then User Verified submission record status changed to "Quoted"

	# Validation : A121_UDX_Currency_Required_Currency_USD
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	Then User Updated 'Submission Currency' field with values 'AMD - Armenian Dram'
	Then User click on Save button 
	Then Verify the Error message "Please confirm if the currency is UDX or not, if yes, Submission Currency must be USD ( Submission Details Tab )"


	Examples:
	| Scenarios  | Record Type                  |
	| Scenario2  | Global Construction          |
	| Scenario3  | Global Offshore              |
	| Scenario5  | International Onshore        |
	| Scenario6  | International Property       |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	| Scenario10 | SGS - Onshore                |
	| Scenario12 | SCI - Crisis Management      |
	| Scenario13 | SCI - Cyber                  |
	| Scenario14 | SCI - Environmental          |
	| Scenario15 | SCI - Financial Lines        |
	| Scenario16 | SCI - GL Misc                |
	| Scenario17 | SCI - Healthcare             |