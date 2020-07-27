import { create } from 'domain';

const HomePage = () => import('views/home/index');

const News = () => import('views/News/News');
const NewsCreate = () => import('views/News/NewsCreate');
const NewsDelete = () => import('views/News/NewsDelete');
const NewsDetail = () => import('views/News/NewsDetail');
const NewsEdit = () => import('views/News/NewsEdit');
const LocationManager = () => import('views/category/location/locationManager');

const City = () => import('views/category/location/city/city');
const CreateOrUpdateCity = () => import('views/category/location/city/createOrUpdate');

const District = () => import('views/category/location/district/district');
const CreateOrUpdateDistrict = () => import('views/category/location/district/createOrUpdate');

const Ward = () => import('views/category/location/ward/ward');
const CreateOrUpdateWard = () => import('views/category/location/ward/createOrUpdate');

const WareHouse = () => import('views/category/warehouse/warehouse');
const CreateOrUpdateWarehouse = () => import('views/category/warehouse/createOrUpdate');

const ProductCategory = () => import('views/category/productcategory/index');
const ProductCategoryCreateOrUpdate = () => import('views/category/productcategory/createorupdate');

const EmptyComponent = () => import('views/EmptyComponent');

const Setting = () => import('views/setting/setting');
const SettingCreateOrUpdate = () => import('views/setting/createorupdate');

const Provider = () => import('views/email/emailProvider/emailProvider');
const ProviderCreateOrUpdate = () => import('views/email/emailProvider/createorupdate');

const Account = () => import('views/email/emailAccount/emailAccount');
const AccountCreateOrUpdate = () => import('views/email/emailAccount/createorupdate');
const ProductList = () => import('views/product/index');
const Product = () => import('views/product/index');
const ProductCreateOrUpdate = () => import('views/product/createorupdate');

const Template = () => import('views/email/emailTemplate/emailTemplate');
const TemplateCreateOrUpdate = () => import('views/email/emailTemplate/createorupdate');

const Email = () => import('views/email/email/email');
const EmailCreateOrUpdate = () => import('views/email/email/createorupdate');

const Order = () => import('views/order/order');
const OrderCreate = () => import('views/order/create');
const OrderEdit = () => import('views/order/edit');
const OrderDetail = () => import('views/order/detail');
const OrderCreateOrUpdate = () => import('views/order/createorupdate');

const OrderItem = () => import('views/orderitem/orderitem');
const OrderItemCretaeAndUpdate = () => import('views/orderitem/createorupdate');

const Image = () => import('views/image/image');
const ImageEdit = () => import('views/image/edit');
const ImageCreate = () => import('views/image/create');
const ImageCreateOrUpdate = () => import('views/image/createorupdate');

const Notification = () => import('views/Notification/notification');
const NotificationCreate = () => import('views/Notification/create');
const NotificationEdit = () => import('views/Notification/edit');
const NotificationDelete = () => import('views/Notification/delete');

const Tracking = () => import('views/Tracking/track');
const TrackingCreate = () => import('views/Tracking/create');
const TrackingEdit = () => import('views/Tracking/edit');
const TrackingDelete = () => import('views/Tracking/delete');

const Shipment = () => import('views/shipment/shipment')
const ShipmentDetails = () => import('views/shipment/details')
const ShipmentCreateOrUpdate = () => import('views/shipment/createorupdate')
const ShipmentDelete = () => import('views/shipment/delete')

const ShipmentItem = () => import('views/ShipmentItem/ShipmentItem')
const ShipmentItemCreateOrUpdate = () => import('views/ShipmentItem/createorupdate')
const ShipmentItemDetails = () => import('views/ShipmentItem/details')
const ShipmentItemDelete = () => import('views/ShipmentItem/delete')


const Campaign = () => import('views/campaigns/campaign/campaign');
const CampaignCreateOrUpdtae = () => import('views/campaigns/campaign/createorupdate');
const CampaignDetails = () => import('views/campaigns/campaign/details');


const CampaignOrder = () => import('views/campaigns/campaignorder/index');
const CampaignOrderCreate = () => import('views/campaigns/campaignorder/create');
const CampaignOrderDetails = () => import('views/campaigns/campaignorder/details');
const CampaignOrderEdit = () => import('views/campaigns/campaignorder/edit');
const CampaignOrderDelete = () => import('views/campaigns/campaignorder/delete');

const CampaignProduct = () => import('views/campaigns/campaignproduct/index');
const CampaignProductCreate = () => import('views/campaigns/campaignproduct/create');
const CampaignProductDetails = () => import('views/campaigns/campaignproduct/details');
const CampaignProductEdit = () => import('views/campaigns/campaignproduct/edit');
const CampaignProductDelete = () => import('views/campaigns/campaignproduct/delete');

const Discount = () => import('views/campaigns/discount/discount');
const DiscountCreateOrUpdate = () => import('views/campaigns/discount/createorupdate');

const DiscountOrder = () => import('views/campaigns/discountorder/index');
const DiscountOrderCreate = () => import('views/campaigns/discountorder/create');
const DiscountOrderDetails = () => import('views/campaigns/discountorder/details');
const DiscountOrderEdit = () => import('views/campaigns/discountorder/edit');
const DiscountOrderDelete = () => import('views/campaigns/campaign/delete');

const Attribute = () => import('views/attributes/attribute/index');
const AttributeCreateOrUpdate = () => import('views/attributes/attribute/createorupdate');

const AttributeValue = () => import('views/attributes/attributevalue/index');
const AttributeValueCreateOrUpdate = () => import('views/attributes/attributevalue/createorupdate');

const SpecAttribute = () => import('views/attributes/specattribute/index');
const SpecAttributeCreateOrUpdate = () => import('views/attributes/specattribute/createorupdate');

const InventoryAttribute = () => import('views/attributes/inventoryattribute/index');
const InventoryAttributeCreateOrUpdate = () => import('views/attributes/inventoryattribute/createorupdate');

const ProductPiece = () => import('views/productpiece/index');
const ProductPieceCreateOrUpdate = () => import('views/productpiece/createorupdate');

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  {
    path: '/setting', component: EmptyComponent,
    children: [
      { path: '/', component: Setting },
      { name: 'settingCreateOrUpdate', path: 'createOrUpdate/:id', component: SettingCreateOrUpdate }
    ]
    , display: 'Quản lý cấu hình'
  },
  {
    path: '/emailprovider', component: EmptyComponent,
    children: [
      { path: '/', component: Provider },
      { name: 'providerCreateOrUpdate', path: 'createOrUpdate/:id', component: ProviderCreateOrUpdate }
    ]
    , display: 'Quản lý nhà cung cấp'
  },
  {
    path: '/emailaccount', component: EmptyComponent,
    children: [
      { path: '/', component: Account },
      { name: 'accountCreateOrUpdate', path: 'createOrUpdate/:id', component: AccountCreateOrUpdate }
    ]
    , display: 'Quản lý tài khoản'
  },
  {
    path: '/emailtemplate', component: EmptyComponent,
    children: [
      { path: '/', component: Template },
      { name: 'templateCreateOrUpdate', path: 'createOrUpdate/:id', component: TemplateCreateOrUpdate }
    ]
    , display: 'Quản lý mẫu'
  },
  {
    path: '/email', component: EmptyComponent,
    children: [
      { path: '/', component: Email },
      { name: 'emailCreateOrUpdate', path: 'createOrUpdate/:id', component: EmailCreateOrUpdate }
    ]
    , display: 'Quản lý email'
  },

  { name: 'locationManager', path: '/locationManager', component: LocationManager, display: 'Quản lý tỉnh/huyện/xã', icon: 'locationManager' },
  { name: 'city', path: '/city', component: City, display: 'Quản lý tỉnh/thành phố', icon: 'city' },
  { name: 'district', path: '/district', component: District, display: 'Quản lý quận/huyện', icon: 'district' },
  { name: 'ward', path: '/ward', component: Ward, display: 'Quản lý phường/xã', icon: 'ward' },
  { name: 'createOrUpdateCity', path: '/createOrUpdate/city/:id', component: CreateOrUpdateCity, display: 'Tạo/Cập Nhật Thành Phố', icon: 'city' },
  { name: 'createOrUpdateDistrict', path: '/createOrUpdate/district/:id', component: CreateOrUpdateDistrict, display: 'Tạo/Cập Nhật Quận', icon: 'district' },
  { name: 'createOrUpdateWard', path: '/createOrUpdate/ward/:id', component: CreateOrUpdateWard, display: 'Tạo/Cập Nhật Phường', icon: 'ward' },
  { name: 'warehouse', path: '/warehouse', component: WareHouse, display: 'Quản lý Nhà Kho', icon: 'warehouse' },
  { name: 'createOrUpdateWarehouse', path: '/createOrUpdate/warehouse/:id', component: CreateOrUpdateWarehouse, display: 'Tạo/Cập Nhật Nhà Kho', icon: 'warehouse' },
  {
    path: '/productcategory',
    component: EmptyComponent,
    children: [{
      path: "/",
      component: ProductCategory
    },{
      path: 'createorupdate/:id',
      component: ProductCategoryCreateOrUpdate
    }],
    display: 'Danh mục sản phẩm',
    icon: 'home',
  },
  {
    path: '/product',
    component: EmptyComponent,
    children: [{
      path: "/",
      component: Product
    },{
      path: "createorupdate/:id",
      component: ProductCreateOrUpdate
    }],
    display: 'Danh sách sản phẩm',
    icon: 'package'
  },
  {
    name: 'city',
    path: '/city',
    component: City,
    display: 'Quản lý thành phố',
    icon: 'city'
  }, {
    name: 'productcreateorupdate',
    path: '/productcreateorupdate',
    component: ProductCreateOrUpdate,
    display: 'Tạo sản phẩm',
    icon: 'package'
  },
  {
    name: 'productlist',
    path: '/productlist',
    component: ProductList,
    display: 'Danh sách sản phẩm',
    icon: 'package'
  },
    {
        path: '/campaign', component: EmptyComponent,
        children: [
            { path: '/', component: Campaign },
            { name: 'CampaignCreateOrUpdtae', path: 'createorupdate/:id', component: CampaignCreateOrUpdtae },
            { name: 'CampaignDetails', path: 'details/:id', component: CampaignDetails }
        ]
        , display: 'Quản lý tài khoản'
    },
    {
        path: '/campaignorder',
        component: EmptyComponent,
        children: [{
            path: "/",
            component: CampaignOrder
        }, {
            path: "create",
            component: CampaignOrderCreate
        },
        {
            path: "details/:id",
            component: CampaignOrderDetails
        }, {
            path: "edit/:id",
            component: CampaignOrderEdit
        },
        {
            path: "delete/:id",
            component: CampaignOrderDelete
        },
        ],
        display: 'Danh sách Order chiến dịch',
        icon: 'package'
    },
    {
        path: '/campaignproduct',
        component: EmptyComponent,
        children: [{
            path: "/",
            component: CampaignProduct
        }, {
            path: "create",
            component: CampaignProductCreate
        },
        {
            path: "details/:id",
            component: CampaignProductDetails
        }, {
            path: "edit/:id",
            component: CampaignProductEdit
        },
        {
            path: "delete/:id",
            component: CampaignProductDelete
        },
        ],
        display: 'Danh sách sản phẩm chiến dịch',
        icon: 'package'
    },
    {
        path: '/discount', component: EmptyComponent,
        children: [
            { path: '/', component: Discount },
            { name: 'DiscountCreateOrUpdate', path: 'createorupdate/:id', component: DiscountCreateOrUpdate }
        ]
        , display: 'Quản lý tài khoản'
    },
    {
        path: '/discountorder',
        component: EmptyComponent,
        children: [{
            path: "/",
            component: DiscountOrder
        }, {
            path: "create",
            component: DiscountOrderCreate
        },
        {
            path: "details/:id",
            component: DiscountOrderDetails
        }, {
            path: "edit/:id",
            component: DiscountOrderEdit
        },
        {
            path: "delete/:id",
            component: DiscountOrderDelete
        },
        ],
        display: 'Danh sách yêu cầu giảm giá',
        icon: 'package'
    },
    { name: 'order', path: '/order', component: Order, display: 'Quản lý đơn hàng', icon: 'order' },
    { name: 'ordercreat', path: '/order/create', component: OrderCreate, display: 'Tạo mới' },
    { name: 'orderedit', path: '/order/edit/:id', component: OrderEdit, display: 'Sửa' },
    { name: 'orderdetail', path: '/order/detail/:id', component: OrderDetail, display: 'Chi tiết' },
    { name: 'ordercreateorupdate', path: '/order/createorupdate/:id', component: OrderCreateOrUpdate, display: 'Tạo mới & Sửa' },

    { name: 'orderitem', path: '/orderitem', component: OrderItem, display: ' đơn hàng' },
    { name: 'orderitemcreateorupdate', path: '/orderitem/createorupdate/:id', component: OrderItemCretaeAndUpdate, display: 'Tạo mới & Sửa' },

    { name: 'image', path: '/image', component: Image, display: 'Quản lý Hình Ảnh', icon: 'image' },
    { name: 'imagecreate', path: '/image/create', component: ImageCreate },
    { name: 'imageedit', path: '/image/edit/:id', component: ImageEdit },
    { name: 'imagecreateorupdate', path: '/image/createorupdate/:id', component: ImageCreateOrUpdate, display: 'Tạo mới & Sửa' },



  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'productcategory', path: '/productcategory', component: ProductCategory, display: 'Danh mục sản phẩm', icon: 'home' },
  { name: 'city', path: '/city', component: City, display: 'Quản lý thành phố', icon: 'city' },
  { name: 'News', path: '/News', component: News, display: 'Quản lý blog', icon: 'News' },
  { name: 'NewsCreate', path: '/NewsCreate', component: NewsCreate, display: 'Tạo mới blog' },
  { name: 'NewsDelete', path: '/NewsDelete', component: NewsDelete, display: 'Xóa blog' },
  { name: 'NewsDetail', path: '/NewsDetail/:id', component: NewsDetail, display: 'hiển thị blog' },
  { name: 'NewsEdit', path: '/NewsEdit/:id', component: NewsEdit, display: 'Sửa blog' },

   { name: 'shipment', path: '/shipment', component: Shipment, display: 'Quản lí đơn hàng', icon: 'shipment' },
  { name: 'DetailsShipment', path: '/shipment/details/:id', component: ShipmentDetails, display: 'Chi tiết đơn hàng' },
  { name: 'CreateOrUpdateShipment', path: '/shipment/createorupdate/:id', component: ShipmentCreateOrUpdate, display: 'Tạo / Sửa đơn hàng' },
  { name: 'DeleteShipment', path: '/shipment/delete/:id', component: ShipmentDelete, display: 'Xóa đơn hàng' },

  { name: 'ShipmentItem', path: '/ShipmentItem', component: ShipmentItem, display: 'Quản lí đơn vận' },
  { name: 'CreateOrUpdateShipmentItem', path: '/ShipmentItem/createorupdate/:id', component: ShipmentItemCreateOrUpdate, display: 'Tạo / Sửa đơn vận' },
  { name: 'DetailsShipmentItem', path: '/ShipmentItem/details/:id', component: ShipmentItemDetails, display: 'Chi tiết đơn vận' },
  { name: 'DeleteShipmentItem', path: '/ShipmentItem/delete/:id', component: ShipmentItemDelete, display: 'Xóa đơn vận' },
  
    { name: 'noti', path: '/notification', component: Notification, display: 'Thông báo', icon: 'noti' },
    { name: 'create', path: '/Notification/create', component: NotificationCreate, display: 'Tạo thông báo', icon: 'noti' },
    { name: 'edit', path: '/Notification/edit/:id', component: NotificationEdit, display: 'sửa thông báo', icon: 'noti' },
    { name: 'del', path: '/Notification/delete/:id', component: NotificationDelete, display: 'Xóa thông báo', icon: 'noti' },
    { name: 'track', path: '/track', component: Tracking, display: 'Lịch sử', icon: 'track' },
    { name: 'trackcreate', path: '/Tracking/create', component: TrackingCreate, display: 'Tạo Lịch sử', icon: 'track' },


    { name: 'trackedit', path: '/Tracking/edit/:id', component: TrackingEdit, display: 'Sửa Lịch sử', icon: 'track' },
    { name: 'trackdel', path: '/Tracking/delete/:id', component: TrackingDelete, display: 'Xóa Lịch sử', icon: 'track' },
  //{ name: 'Campaign', path: '/Campaign', component: Campaign, display: 'Quản lý chiến dịch', icon: 'campaign' },
  //{ name: 'CampaignCreate', path: '/CampaignCreate', component: CampaignCreate, display: 'Tạo mới' },
  //{ name: 'CampaignDelete', path: '/CampaignDelete', component: CampaignDelete, display: 'Xóa chiến dịch' },
  //{ name: 'CampaignDetail', path: '/CampaignDetail/:id', component: CampaignDetail, display: 'hiển thị' },
  //{ name: 'CampaignEdit', path: '/CampaignEdit/:id', component: CampaignEdit, display: 'Sửa' },
    {
    path: '/attribute',
    component: EmptyComponent,
    children: [{
      path: "/",
      component: Attribute
    },{
        path: 'createorupdate/:id',
        component: AttributeCreateOrUpdate
    }],
    display: 'Thuộc tính sản phẩm',
    icon: 'home',
  },
  {
    path: '/attributevalue',
    component: EmptyComponent,
    children: [{
      path: "/",
      component: AttributeValue
    }, {
        path: 'createorupdate/:id',
        component: AttributeValueCreateOrUpdate
    }],
    display: 'Giá trị thuộc tính',
    icon: 'home',
  }, {
    path: '/specattribute',
    component: EmptyComponent,
    children: [{
      path: "/",
      component: SpecAttribute
    }, {
        path: 'createorupdate/:id',
        component: SpecAttributeCreateOrUpdate
    }],
    display: 'Thuộc tính đặc biệt',
    icon: 'home',
  }, {
    path: '/inventoryattribute',
    component: EmptyComponent,
    children: [{
      path: "/",
      component: InventoryAttribute
    }, {
        path: 'createorupdate/:id',
        component: InventoryAttributeCreateOrUpdate
    }],
    display: 'Thuộc tính kho',
    icon: 'home',
  }, {
    path: '/productpiece',
    component: EmptyComponent,
    children: [{
      path: "/",
      component: ProductPiece
    }, {
      path: 'createorupdate/:id',
      component: ProductPieceCreateOrUpdate
    }],
    display: 'Đơn vị sản phẩm',
    icon: 'home',
  },
]

