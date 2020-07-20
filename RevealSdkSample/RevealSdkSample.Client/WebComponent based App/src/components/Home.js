import React, { Component } from "react";

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <div>
          <h3>Points of interest in this sample:</h3>
          <ul>
            <li>
              index.html file contains loading of RevealSdk scripts as well as
              its dependencies. There is specified the base URL of the backend
              server.
            </li>
            <li>
              You could find guidance on how RevealSdk theming works also in
              index.html. It also changes background color to be grean.
              <a href="https://www.infragistics.com/community/blogs/b/infragistics/posts/reveal-custom-theme-blog">
                {" "}
                This blog post
              </a>{" "}
              explains in more detail how the theming works.
            </li>
            <li>
              Filters component shows host app interactions with Reveal
              dashboard filters.
            </li>
            <li>
              Linking component renders a marketing dashboard. On it's last
              visualization(New Seats by Campaign) you see a grid, where the
              last column is setup to be a link one. When the end user clicks on
              link for a particular row a popup is shown where at the bottom of
              it there is link saing "Open Campaings", when you click at it the
              onVisualizationLinkingDashboard event is fired and in its details
              contains a callback property. You need to call this callback and
              provide dashboard ID that would be requested from the backend and
              rendered.
            </li>
            <li>This samples utilizes the RevealSdk web-component.</li>
          </ul>
          <h3>Known issues:</h3>
          <ul>
            <li>
              On initial load some of the titles and text rendered by reveal
              view appear trimmed - this is a result of the Roboto fonts(used by
              Reveal) are still not loaded and another font is used during
              layout.
            </li>
          </ul>
        </div>
      </div>
    );
  }
}
