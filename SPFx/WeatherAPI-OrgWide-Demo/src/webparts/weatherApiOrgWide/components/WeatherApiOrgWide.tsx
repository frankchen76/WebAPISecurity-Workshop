import * as React from 'react';
import styles from './WeatherApiOrgWide.module.scss';
import { IWeatherApiOrgWideProps } from './IWeatherApiOrgWideProps';
import { escape } from '@microsoft/sp-lodash-subset';
import { PrimaryButton, Stack, Spinner } from 'office-ui-fabric-react';
import { IWeatherApiOrgWideState } from './IWeatherApiOrgWideState';
import { AadHttpClient, HttpClient } from '@microsoft/sp-http';

export default class WeatherApiOrgWide extends React.Component<IWeatherApiOrgWideProps, IWeatherApiOrgWideState> {
  constructor(props: IWeatherApiOrgWideProps) {
    super(props);
    this.state = {
      loading: false,
      jsonResult: undefined
    };
  }

  private _customAPIHandler = async (): Promise<void> => {
    try {
      this.setState({
        loading: true
      });
      const url = 'https://frankchenpfetest-weatherapi.azurewebsites.net/weatherforecast';

      const aadHttpClient = await this.props.context
        .aadHttpClientFactory
        .getClient('api://f74dd2f3-40d5-4313-b2a4-2b170be9500f');
      const response = await aadHttpClient.get(url, AadHttpClient.configurations.v1);
      if (!response.ok) {
        const err = await response.text();
        throw new Error(err);
      }

      const result = await response.json();
      this.setState({
        loading: false,
        jsonResult: JSON.stringify(result, null, 4)
      });
    } catch (error) {
      this.setState({
        loading: false,
        jsonResult: JSON.stringify(error, null, 4)
      });
    }
  }

  public render(): React.ReactElement<IWeatherApiOrgWideProps> {
    return (
      <div className={styles.weatherApiOrgWide}>
        <div className={styles.container}>
          <div className={styles.row}>
            <div className={styles.column}>
              <span className={styles.title}>MS Graph API Demo</span>
              <Stack horizontal disableShrink horizontalAlign="space-evenly">
                <PrimaryButton text="Customer API Test" onClick={this._customAPIHandler} />
              </Stack>
            </div>
          </div>
          <div className={styles.row}>
            <div className={styles.column}>
              {
                this.state.loading ?
                  <Spinner label="loading..." />
                  :
                  <div className={styles.jsoncode}>{this.state.jsonResult}</div>
              }
            </div>
          </div>
        </div>
      </div>
    );
  }
}
