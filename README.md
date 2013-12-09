Responsive Device Resolver
======================

<b>The Responsive Device Resolver module allows you to create rules that change the Context Device based on a user's detected screen resolution. This is an alternative approach that allows you to use Sitecore Devices without relying on device detection and external User Agent DB services that require subscription fees.</b>

This module gives you the ability to take advantage of the built in Sitecore device functionality without using User agent sniffing. Instead the user's screen resolution is used to set the Context Device based on rules that are configured with the rules engine. A good use case for this would be to serve different content (presentation components or datasources) to users if they are using a device with a small resolution (Ex: tablet and/or mobile phone).

After installation, there is one line that needs to be added to your Main Layout file(s). The following line adds a sublayout file to your main layout that takes care of setting a cookie to keep track of screen resolution.

<pre><sc:sublayout runat="server"  path="/layouts/Responsive Device Resolver/Responsive Device Resolver.ascx" id="RDRsublayout" /></pre>

The next step is to navigate to the Responsive Device Resolver item located in the modules section of the content tree (/sitecore/system/Modules/Responsive Device Resolver). This item allows you to set global rules that will determine which devices are triggered for different detected screen resolutions. It is important to note that there is also an added rule that will make sure that a device is only triggered if the Context Item has Layout details applied. This allows you to set global Device rules that may only be used on a subset of items. Just add a layout and different presentation details to the items you want to trigger on different devices.

Keep in mind that the "screen.width" and "screen.height" JS methods are used to detect device resolution in this module. These values give us a good chance to correctly determine a user's device resolution but they are not always accurate. The good news is that even if the numbers are slightly off, we can still make an informed guess as to the user's device. More testing info will be made available as soon as possible.

Thanks to John West. His post was an excellent starting point for this project: http://www.sitecore.net/Community/Technical-Blogs/John-West-Sitecore-Blog/Posts/2010/11/Using-the-Sitecore-Rules-Engine-in-a-Custom-Context-Setting-the-Context-Device.aspx