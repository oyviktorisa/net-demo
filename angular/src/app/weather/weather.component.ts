import {
    Component,
    Injector,
    OnInit,
    EventEmitter,
    Output
} from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { finalize } from 'rxjs/operators';
import { forEach as _forEach, map as _map } from 'lodash-es';
import {
    WeatherServiceProxy,
    GetWeatherDto,
    LocationDto,
    LocationDtoResultDto,
    WeatherDto
} from '@shared/service-proxies/service-proxies';
import { AbpValidationError } from '@shared/components/validation/abp-validation.api';

@Component({
  templateUrl: './weather.component.html'
})
export class WeatherComponent extends AppComponentBase
    implements OnInit {
    submitting = false;
    query = new GetWeatherDto();
    countries: LocationDto[];
    cities: LocationDto[];
    selectedCountry: string | undefined;
    selectedCity: string | undefined;
    weather: WeatherDto;

  constructor(
    injector: Injector,
    public weatherService: WeatherServiceProxy
  ) {
    super(injector);
  }
    ngOnInit(): void {
        this.weatherService
            .getLocation(undefined)
            .subscribe((result: LocationDtoResultDto) => {
                this.countries = result.items;
            });
    }

    onCountrySelected() {
        this.weatherService
            .getLocation(this.selectedCountry)
            .subscribe((result: LocationDtoResultDto) => {
                this.cities = result.items;
            });
    }

    check(): void {
        this.submitting = true;
        this.query.country = this.selectedCountry;
        this.query.city = this.selectedCity;

        this.weatherService
            .getCurrentWeather(this.query)
            .subscribe((result: WeatherDto) => {
                this.weather = result;
            });

        this.submitting = false;
    }
}
