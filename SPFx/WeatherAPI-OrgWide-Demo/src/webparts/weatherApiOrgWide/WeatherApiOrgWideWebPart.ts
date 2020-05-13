import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-property-pane';
import { BaseClientSideWebPart } from '@microsoft/sp-webpart-base';

import * as strings from 'WeatherApiOrgWideWebPartStrings';
import WeatherApiOrgWide from './components/WeatherApiOrgWide';
import { IWeatherApiOrgWideProps } from './components/IWeatherApiOrgWideProps';

export interface IWeatherApiOrgWideWebPartProps {
  description: string;
}

export default class WeatherApiOrgWideWebPart extends BaseClientSideWebPart<IWeatherApiOrgWideWebPartProps> {

  public render(): void {
    const element: React.ReactElement<IWeatherApiOrgWideProps> = React.createElement(
      WeatherApiOrgWide,
      {
        description: this.properties.description,
        context: this.context
      }
    );

    ReactDom.render(element, this.domElement);
  }

  protected onDispose(): void {
    ReactDom.unmountComponentAtNode(this.domElement);
  }

  protected get dataVersion(): Version {
    return Version.parse('1.0');
  }

  protected getPropertyPaneConfiguration(): IPropertyPaneConfiguration {
    return {
      pages: [
        {
          header: {
            description: strings.PropertyPaneDescription
          },
          groups: [
            {
              groupName: strings.BasicGroupName,
              groupFields: [
                PropertyPaneTextField('description', {
                  label: strings.DescriptionFieldLabel
                })
              ]
            }
          ]
        }
      ]
    };
  }
}
