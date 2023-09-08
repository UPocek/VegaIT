# VegaIT TimeSheet full-stack application

![Main](https://github.com/UPocek/VegaIT/blob/main/docs/main.png)

## Requirements

### Back-end

![Back](https://github.com/UPocek/VegaIT/blob/main/docs/swager.png)

The technology requirement for this part was C# .NET API with the ability to sustain a high load within a short period of time when all employees finish work day and want to report their work in a timesheet, so I decided to make business logic and information processing for this application Async. Async connections will enable better concurrent connections and can sustain a higher number of simultaneous requests which is very important for this type of application. Next for onboarding new employees and for password reset options integration with SendGrid was required, that part was implemented successfully with a custom template email that is fun and easy to navigate. Integration with the front-end was as straightforward as it gets and connection with the database and EntityFramework will be explained in more detail in the Dokcer section...

### Front-end 

![Back](https://github.com/UPocek/VegaIT/blob/main/docs/login.png)
![Back](https://github.com/UPocek/VegaIT/blob/main/docs/swager.png)
![Back](https://github.com/UPocek/VegaIT/blob/main/docs/categories.png)
![Back](https://github.com/UPocek/VegaIT/blob/main/docs/reports.png)

The front was coded in the React javascript library with the framework NextJS for different optimization benefits. A new thing for me in this project was strict VegaIT react code guidelines that I needed to follow and eslint rules that were guiding me along the way. The key takeaway from this experience is "Don't go anywhere without good eslint rules :)" After I got used to it, it was extremely helpful and the code really looks cleaner and is easier to read and manage later. Next up, after finishing all the screens and reports was optimization for which I used different techniques. React provides different hooks that can make your front-end application much faster, where the most important are useMemo() for memoization (caching) of variables and useCallback() for memoization of function definitions which can be used to reduce the number of component re-renders and boost performance. Besides those two things, everything else was pretty standard and as I already had a couple of projects behind me in React and NextJS I was able to finish this part quite fast and easily. 
