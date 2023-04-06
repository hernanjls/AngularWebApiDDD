import { TableColumn } from "src/@vex/interfaces/table-column.interface";
import { Task } from "src/app/pages/task/models/task-response.interface";
import ictask from "@iconify/icons-ic/twotone-task";
import { ListTableMenu } from "src/app/commons/list-table-menu.interface";
import icViewHeadline from "@iconify/icons-ic/twotone-view-headline";
import icLabel from "@iconify/icons-ic/twotone-label";
import icCalendarMonth from "@iconify/icons-ic/twotone-calendar-today";
import { GenericValidators } from "@shared/validators/generic-validators";
import { TableColumns } from "@shared/models/list-table.interface";
import { SearchOptions } from "@shared/models/search-options.interface";
import { MenuItems } from "@shared/models/menu-items.interface";

const searchOptions: SearchOptions[] = [
  {
    label: "Name",
    value: 1,
    placeholder: "Search by Name",
    validation: [GenericValidators.defaultName],
    validation_desc: "Only letters are allowed in this search.",
    icon: "icName"
  },
  {
    label: "Description",
    value: 2,
    placeholder: "Search by Description",
    validation: [GenericValidators.defaultDescription],
    validation_desc: "Only letters and numbers are allowed in this search.",
    icon: "icDescription"
  },
];

const menuItems: MenuItems[] = [
  {
    type: "link",
    id: "all",
    icon: icViewHeadline,
    label: "All",
  },
  {
    type: "link",
    id: "Activo",
    value: 1,
    icon: icLabel,
    label: "Active",
    class: {
      icon: "text-green",
    },
  },
  {
    type: "link",
    id: "Inactivo",
    value: 0,
    icon: icLabel,
    label: "Inactive",
    class: {
      icon: "text-gray",
    },
  },
];

const tableColumns: TableColumns<Task>[] = [
  {
    label: "NAME",
    cssLabel: ["font-bold", "text-sm"],
    property: "name",
    cssProperty: ["font-semibold", "text-sm", "text-left"],
    type: "text",
    sticky: true,
    sort: true,
    sortProperty: "name",
    visible: true,
    download: true,
  },
  {
    label: "DESCRIPTION",
    cssLabel: ["font-bold", "text-sm"],
    property: "description",
    cssProperty: ["font-semibold", "text-sm", "text-left"],
    type: "text",
    sticky: false,
    sort: true,
    sortProperty: "description",
    visible: true,
    download: true,
  },
  {
    label: "CREATION DATE",
    cssLabel: ["font-bold", "text-sm"],
    property: "auditCreateDate",
    cssProperty: ["font-semibold", "text-sm", "text-left"],
    type: "datetime",
    sticky: false,
    sort: true,
    visible: true,
    download: true,
  },
  {
    label: "STATUS",
    cssLabel: ["font-bold", "text-sm"],
    property: "stateTask",
    cssProperty: ["font-semibold", "text-sm", "text-left"],
    type: "badge",
    sticky: false,
    sort: false,
    visible: true,
    download: true,
  },
  {
    label: "",
    cssLabel: [],
    property: "icEdit",
    cssProperty: [],
    type: "icon",
    action: "edit",
    sticky: false,
    sort: false,
    visible: true,
    download: false,
  },
  {
    label: "",
    cssLabel: [],
    property: "icDelete",
    cssProperty: [],
    type: "icon",
    action: "remove",
    sticky: false,
    sort: false,
    visible: true,
    download: false,
  },
];

const filters = {
  numFilter: 0,
  textFilter: "",
  stateFilter: null,
  startDate: null,
  endDate: null,
};

const inputs = {
  numFilter: 0,
  textFilter: "",
  stateFilter: null,
  startDate: null,
  endDate: null,
};

export const componentSettings = {
  // ICONS
  ictask: ictask,
  icCalendarMonth: icCalendarMonth,
  // LAYOUT SETTINGS
  menuOpen: false,
  // TABLE SETTINGS
  tableColumns: tableColumns,
  initialSort: "Id",
  initialSortDir: "desc",
  getInputs: inputs,
  buttonLabel: "EDIT",
  buttonLabel2: "DELETE",
  // SEARCH FILTROS
  menuItems: menuItems,
  searchOptions: searchOptions,
  filters_dates_active: false,
  filters: filters,
  datesFilterArray: ["Creation date"],
  columnsFilter: tableColumns.map((column) => {
    return {
      label: column.label,
      property: column.property,
      type: column.type,
    };
  }),
};
