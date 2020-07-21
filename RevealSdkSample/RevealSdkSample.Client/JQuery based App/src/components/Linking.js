import React, { useRef, useEffect } from "react";
const $ = window.$;

export const Linking = (props) => {
  const element = useRef(null);

  function handleLinkingDashboard(event) {
    event.detail.callback("Campaigns");
  }

  const initBoard = () => {
    const dashboardId = "Marketing";
    const settings = new $.ig.RevealSettings(dashboardId);
    $.ig.RevealUtility.loadDashboard(dashboardId, (dashboard) => {
      settings.dashboard = dashboard;
      new $.ig.RevealView("#marketing", settings);
    }, (error) => console.log(error));
  };

  useEffect(() => {
    initBoard();
    const el = element.current;
    el.addEventListener('onVisualizationLinkingDashboard', handleLinkingDashboard);
    return () => {
      el.removeEventListener('onVisualizationLinkingDashboard', handleLinkingDashboard);
    };
  }, []);

  return (
    <div id="marketing" ref={element} />
  );
}
