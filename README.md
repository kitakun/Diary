# 'WriteItDown' Diary
Just a diary for myself usage

# To implement:
 * Edit existing records
 * Delete existing records
 * Change space settings (make it private or even delete)
 * Implement 'last update space' feature
 * Filter by date ranges (right now only by 1 date)
 * Text preview on all list
 * Improve styles (create new record is merged with view page)
 * Add controls:
   - Rating
      * 5 of 5
      * 10 of 10
    - Amount (money, ints, ect)
 * Add reports
   - Calc sum of tag values from controls

# Used in project on server-side
 * Autofac
 * EF

 Last version from 27.01.2020

# First version:

The site was attempted to use multiple URLs, for example:
diary.com - main preview page
jon.diary.com - Jon's diary page

## .NET stack:
 * Autofac v4
 * EF
 * NET Core 2.2
 * Razor pages

 ## Currently implemented:
 * Authentication through external services (vk.ru only)
 * Create Self-space (bound by user login)
   - This space can be protected by password
   - This space can be accessed only via link
   - This space can be visible for all (with this feature space will be available from home for any users)
 * In space we have filters:
   - Filter 'Visible all accessable'
   - Filter 'Visible all and with passwords'
   - Filter 'Visible only with passwords'
   - Filter by date
   - Filter by tags
 * Records can contains infinite tags count, later this tags will popup in tag control for this space (separated with other spaces)
 * Text body wysiwyg implemented with https://quilljs.com/
 * All styles implemented with `bootstrap 4`
 * Records with password on it will encrypt message body, password for this messages don't stored anywhere and can't be restored

## Used in project on server-side
 * Autofac
 * EF
